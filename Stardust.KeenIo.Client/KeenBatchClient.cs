#region license header
//
// keenbatchclient.cs
// This file is part of Stardust.KeenIo.Client
//
// Author: Jonas Syrstad (jonas.syrstad@dnvgl.com), http://no.linkedin.com/in/jonassyrstad/) 
// Copyright (c) 2016 Jonas Syrstad. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#endregion license header
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

        private static ILogger logger;

        static KeenBatchClient()
        {
            batchEventCollector = ProxyFactory.CreateInstance<IBatchEventCollector>(KeenClient.baseUrl);
        }

        public static async void AddEvent(string collection, object eventEntry)
        {
            Stopwatch timer = null;
            if (VerboseLogging)
                timer = Stopwatch.StartNew();
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
            lock (counterLock)
            {
                shouldFlush = batchCollector.Count > batchSize || lastPush < DateTime.UtcNow.AddMinutes(-5);
            }
            if (VerboseLogging && timer != null)
            {
                timer.Stop();
                Logger?.Message($"Adding event: {timer.ElapsedMilliseconds}");
            }
            if (shouldFlush)
            {
                timer?.Restart();
                FlushInternal();
                if (VerboseLogging && timer != null)
                {
                    timer.Stop();
                    Logger?.Message($"Flushing event cache: {timer.ElapsedMilliseconds}");
                }
            }
        }

        private static void FlushInternal()
        {
            if (PushingEvents) return;
            lock (lockObject)
            {
                if (PushingEvents)
                {

                    return;
                }
                copyingEventCache = true;
                batchCollectorProcessing = batchCollector;
                batchCollector = new ConcurrentBag<BatchEventItem>();
                copyingEventCache = false;
            }
            PushBatch();
        }

        private static void PushBatch()
        {
            Task.Run(
                async () =>
                    {

                        Stopwatch timer = null;
                        if (VerboseLogging)
                            timer = Stopwatch.StartNew();
                        var lst = new List<BatchEventItem>();
                        while (!batchCollectorProcessing.IsEmpty)
                        {
                            BatchEventItem item;
                            batchCollectorProcessing.TryTake(out item);
                            lst.Add(item);
                        }
                        await PushInternal(lst);
                        batchCollectorProcessing = null;
                        if (VerboseLogging && timer != null)
                        {
                            timer.Stop();
                            Logger?.Message($"Pushing {lst.Count} events to keen in {timer.ElapsedMilliseconds}ms");
                        }
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
                    lock (counterLock)
                    {
                        lastPush = DateTime.UtcNow;
                    }
                }
                lock (lockObject)
                {
                    batchCollectorProcessing = null;
                }
            }
            catch (Exception ex)
            {
                Logger?.Error(ex);
                lock (lockObject)
                {
                    batchCollectorProcessing = null;
                }
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
                if (VerboseLogging) Logger?.Message("Shutting down the event pump.");
                collectionTime.Stop();
                eventPumpActive = false;
                while (HasPendingEvents)
                {
                    if (!PushingEvents)
                    {
                        if (VerboseLogging) Logger?.Message("flushing event stores.");
                        Flush();
                        await Task.Delay(100);
                    }
                    await Task.Delay(10);

                }
            }

            catch (Exception ex)
            {
                Logger?.Error(ex);
                if (!KeenClient.SwallowException)
                    throw;
            }
        }

        public static void ShutdownEventPump() => Task.Run(async () => await ShutdownEventPumpAsync()).Wait();

        public static void StartEventPump(bool throwExceptionsAfterShutdown = false)
        {
            ProxyFactory.CreateProxy(typeof(IBatchEventCollector));
            eventPumpActive = true;
            if (VerboseLogging) Logger?.Message("Starting event pump.");
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
            if (VerboseLogging) Logger?.Message("Pausing event pump, no more events are collected until resume is called");
            lock (lockObject)
            {
                collectionTime.Stop();
                paused = true;
            }
        }

        // ReSharper disable once UnusedMember.Global
        public static void Resume()
        {
            if (VerboseLogging) Logger?.Message("Resuming event pump");
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

        private static ILogger Logger
        {
            get
            {
                if (!VerboseLogging) return null;
                if(Logger==null) logger = ExtensionsFactory.GetService<ILogger>();
                return Logger;
            }
        }
    }
}