using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.Query;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client
{
    public static class KeenClient
    {
        private static string baseUrl = "https://api.keen.io";

        private static string projectId = "";

        public static bool SwalowException { get; set; } = true;

        public static void SetProjectId(string project)
        {
            projectId = project;
        }

        public static void SetReaderKey(string key)
        {
            KeenGlobalConfig.SetReaderKey(key);
        }

        public static void SetWriterKey(string key)
        {
            KeenGlobalConfig.SetWriterKey(key);
        }

        public static void AddEvent(string collection, dynamic value)
        {
            AddEventAsync(collection, value);
        }

        public static async Task AddEventAsync(string collection, dynamic value)
        {
            try
            {
                await Collector.AddEvent(projectId, collection, value);
            }
            catch (Exception)
            {
                if (!SwalowException)
                    throw;
            }
        }


        public static void AddEvents(IDictionary<string, IEnumerable<object>> items)
        {
            AddEventsAsync(items);
        }

        public static async Task AddEventsAsync(IDictionary<string, IEnumerable<object>> items)
        {
            try
            {
                await Collector.AddEvents(projectId, items);
            }
            catch (Exception)
            {
                if (!SwalowException)
                    throw;
            }
        }

        public static async Task AddEventsAsync(string collection, IEnumerable<object> items)
        {
            await AddEventsAsync(new Dictionary<string, IEnumerable<object>> { { collection, items } });
        }

        public static void AddEvents(string collection, IEnumerable<object> items)
        {
            AddEventsAsync(new Dictionary<string, IEnumerable<object>> { { collection, items } });
        }

        public static async Task<CollectionInfo> GetCollectionAsync(string collection)
        {
            return await KeenInspector.GetCollectionAsync(projectId, collection);
        }

        private static IKeenInspection KeenInspector
        {
            get
            {
                return ProxyFactory.CreateInstance<IKeenInspection>(baseUrl);
            }
        }

        public static CollectionInfo GetCollection(string collection)
        {
            return KeenInspector.GetCollection(projectId, collection);
        }

        public static async Task<IEnumerable<CollectionInfo>> GetCollectionsAsync()
        {
            return await KeenInspector.GetAllCollectionAsync(projectId);
        }

        public static IEnumerable<CollectionInfo> GetCollections()
        {
            return KeenInspector.GetAllCollection(projectId);
        }

        public static async Task<dynamic> QueryAsync(this QueryType queryType, QueryBody query)
        {
            return await KeenInspector.QueryAsync(projectId, queryType, query);
        }

        public static dynamic Query(this QueryType queryType, QueryBody query)
        {
            return KeenInspector.Query(projectId, queryType, query);
        }

        public static IEventCollector SetGlobalProperties(string name, object value)
        {
            return Collector.SetGlobalProperty(name, value);
        }

        private static IEventCollector Collector
        {
            get
            {
                return ProxyFactory.CreateInstance<IEventCollector>(baseUrl);
            }
        }
    }
}
