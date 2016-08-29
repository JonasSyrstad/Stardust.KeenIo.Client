using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Client;
using Stardust.Interstellar.Rest.Common;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client
{
    public static class KeenBatchClient
    {
        private static object lockObject = new object();

        private static ConcurrentBag<BatchEventItem> batchCollector = new ConcurrentBag<BatchEventItem>();

        private static ConcurrentBag<BatchEventItem> batchCollectorProcessing;

        private static int batchSize=100;

        private static bool pushingEvents => batchCollectorProcessing != null;

        private static bool eventPumpActive;

        private static bool throwExceptions;

        private static volatile bool copyingEventCache;

        public static async void AddEvent(string collection, object eventEntry)
        {
            if (!eventPumpActive)
            {
                if (throwExceptions)
                    throw new InvalidAsynchronousStateException("Event pump is not running");
                return;
            }
            if (copyingEventCache) await Task.Delay(2);
            batchCollector.Add(new BatchEventItem { Collection = collection, EventEntry = eventEntry });
            if (batchCollector.Count > batchSize)
            {
                FlushInternal();
            }
        }

        private static void FlushInternal()
        {
            if (pushingEvents) return;
            lock (lockObject)
            {
                copyingEventCache = true;
                batchCollectorProcessing = batchCollector;
                batchCollector = new ConcurrentBag<BatchEventItem>();
                copyingEventCache = false;
            }
            var lst = new List<BatchEventItem>();
            while (!batchCollectorProcessing.IsEmpty)
            {
                BatchEventItem item;
                batchCollectorProcessing.TryTake(out item);
                lst.Add(item);
            }
            PushBatch(lst);
        }

        private static void PushBatch(List<BatchEventItem> lst)
        {
            Task.Run(async () => await PushInternal(lst));
        }

        private static async Task PushInternal(List<BatchEventItem> lst)
        {
            try
            {
                var grouped = lst.GroupBy(g => g.Collection);
                await ProxyFactory.CreateInstance<IEventCollector>(KeenClient.baseUrl).AddEvents(KeenClient.projectId, grouped.ToDictionary(k => k.Key, v => v.Select(s => s.EventEntry)));
                lock (lockObject)
                {
                    batchCollectorProcessing = null;
                }
            }
            catch (System.Exception ex)
            {
                ExtensionsFactory.GetService<ILogger>()?.Error(ex);
                lock (lockObject)
                {
                    batchCollectorProcessing = null;
                }
            }
        }

        public static void Flush()
        {
            FlushInternal();
        }

        public static bool HasPendingEvents => pushingEvents || !batchCollector.IsEmpty;

        public static async Task ShutdownEventPumpAsync()
        {
            eventPumpActive = false;
            while (HasPendingEvents)
            {
                if (!pushingEvents)
                {
                    Flush();
                    await Task.Delay(100);
                }
                await Task.Delay(100);

            }
        }

        public static void ShutdownEventPump()
        {
            Task.Run(async () => await ShutdownEventPumpAsync()).Wait();
        }

        public static void StartEventPump(bool throwExceptionsAfterShutdown = false)
        {
            eventPumpActive = true;
            ThrowExceptions(throwExceptionsAfterShutdown);
        }

        public static void ThrowExceptions(bool state)
        {
            throwExceptions = state;
        }

        public static void SetBatchSize(int size)
        {
            batchSize = size;
        }
    }
}