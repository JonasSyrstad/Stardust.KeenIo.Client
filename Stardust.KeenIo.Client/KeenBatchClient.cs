using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Client;
using Stardust.Interstellar.Rest.Common;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client
{
    public static class KeenBatchClient
    {
        public static bool VerboseLogging { get; set; }
        private static readonly object lockObject = new object();
        private static readonly object counterLock = new object();

        private static ConcurrentBag<BatchEventItem> batchCollector = new ConcurrentBag<BatchEventItem>();

        private static ConcurrentBag<BatchEventItem> batchCollectorProcessing;

        private static int batchSize = 100;

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool PushingEvents => batchCollectorProcessing != null;

        private static bool eventPumpActive;

        private static bool throwExceptions;

        private static volatile bool copyingEventCache;

        private static volatile bool paused;

        private static Stopwatch collectionTime;

        private static DateTime lastPush = DateTime.UtcNow;

        private static IBatchEventCollector batchEventCollector;

        static KeenBatchClient()
        {
            batchEventCollector = ProxyFactory.CreateInstance<IBatchEventCollector>(KeenClient.baseUrl);
        }

        public static async void AddEvent(string collection, object eventEntry)
        {
            if (!eventPumpActive)
            {
                if (throwExceptions)
                    throw new InvalidAsynchronousStateException("Event pump is not running");
                ExtensionsFactory.GetService<ILogger>()?.Message("Event pump is not running");
                return;
            }
            if (Paused) return;
            EventsCollected++;
            while (copyingEventCache) await Task.Delay(1);
            var eventItemCloned = GlobalParameterExtensions.AppendGlobalParameters(typeof(IEventCollector).FullName, eventEntry, 0);
            batchCollector.Add(new BatchEventItem { Collection = collection, EventEntry = eventItemCloned });
            bool shouldFlush;
            //lock (counterLock)
            {
                shouldFlush = batchCollector.Count > batchSize || lastPush < DateTime.UtcNow.AddMinutes(-5);
            }
            if (shouldFlush)
            {
                FlushInternal();
            }
        }

        private static void FlushInternal()
        {
            if (PushingEvents) return;
            if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message($"taking lock {nameof(FlushInternal)}");
            lock (lockObject)
            {
                if (PushingEvents)
                {

                    if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message("Lock released FlushInternal no push");
                    return;
                }
                copyingEventCache = true;
                batchCollectorProcessing = batchCollector;
                batchCollector = new ConcurrentBag<BatchEventItem>();
                copyingEventCache = false;
            }
            if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message("Lock released FlushInternal");
            PushBatch();
        }

        private static void PushBatch()
        {
            Task.Run(
                async () =>
                    {
                        var lst = new List<BatchEventItem>();
                        while (!batchCollectorProcessing.IsEmpty)
                        {
                            BatchEventItem item;
                            batchCollectorProcessing.TryTake(out item);
                            lst.Add(item);
                        }
                        await PushInternal(lst);
                        batchCollectorProcessing = null;
                    });
        }

        private static async Task PushInternal(IEnumerable<BatchEventItem> lst)
        {
            try
            {
                if (!Paused)
                {
                    var grouped = lst.GroupBy(g => g.Collection);
                    await batchEventCollector.AddEvents(KeenClient.projectId, grouped.ToDictionary(k => k.Key, v => v.Select(s => s.EventEntry))).ConfigureAwait(false);
                    //lock (counterLock)
                    {
                        if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message("lastPush PushInternal");
                        lastPush = DateTime.UtcNow;
                    }
                }
                if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message($"taking lock {nameof(PushInternal)}");
                lock (lockObject)
                {
                    batchCollectorProcessing = null;
                }
                if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message("Lock released PushInternal");
            }
            catch (Exception ex)
            {
                ExtensionsFactory.GetService<ILogger>()?.Error(ex);
                if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message($"taking lock {nameof(PushInternal)}");
                lock (lockObject)
                {
                    batchCollectorProcessing = null;
                }
                if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message("Lock released PushInternal exeption handler");
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static void Flush() => FlushInternal();

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool HasPendingEvents => PushingEvents || !batchCollector.IsEmpty;

        // ReSharper disable once MemberCanBePrivate.Global
        public static long EventsCollected { get; private set; }

        // ReSharper disable once MemberCanBePrivate.Global
        public static async Task ShutdownEventPumpAsync()
        {
            try
            {
                if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message("Shutting down the event pump.");
                collectionTime.Stop();
                eventPumpActive = false;
                while (HasPendingEvents)
                {
                    if (!PushingEvents)
                    {
                        if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message("flushing event stores.");
                        Flush();
                        await Task.Delay(100);
                    }
                    await Task.Delay(10);

                }
                if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message($"taking lock {nameof(ShutdownEventPumpAsync)}");
                lock (lockObject)
                {
                    if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message($"Having lock {nameof(ShutdownEventPumpAsync)}");
                }
                if (VerboseLogging) ExtensionsFactory.GetService<ILogger>()?.Message($"lock released {nameof(ShutdownEventPumpAsync)}");
            }

            catch (Exception ex)
            {
                ExtensionsFactory.GetService<ILogger>()?.Error(ex);
                if (!KeenClient.SwallowException)
                    throw;
            }
        }

        public static void ShutdownEventPump() => Task.Run(async () => await ShutdownEventPumpAsync()).Wait();

        public static void StartEventPump(bool throwExceptionsAfterShutdown = false)
        {
            ProxyFactory.CreateProxy(typeof(IBatchEventCollector));
            ExtensionsFactory.GetService<ILogger>()?.Message("Starting event pump.");
            eventPumpActive = true;
            ThrowExceptions(throwExceptionsAfterShutdown);
            if (collectionTime == null) collectionTime = Stopwatch.StartNew();
            else collectionTime.Start();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static void ThrowExceptions(bool state) => throwExceptions = state;

        public static void SetBatchSize(int size)
        {
            batchSize = size;
        }

        /// <summary>
        /// Pauses event collections. Events will be written until Resume is called.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static void Pause()
        {
            ExtensionsFactory.GetService<ILogger>()?.Message("Pausing event pump, no more events are collected until resume is called");
            lock (lockObject)
            {
                collectionTime.Stop();
                paused = true;
            }
        }

        // ReSharper disable once UnusedMember.Global
        public static void Resume()
        {
            ExtensionsFactory.GetService<ILogger>()?.Message("Resuming event pump");
            lock (lockObject)
            {
                paused = false;
                collectionTime.Start();
            }
        }

        // ReSharper disable once UnusedMember.Global
        public static TimeSpan? CollectionFor => collectionTime?.Elapsed;

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool Paused => paused;
    }
}