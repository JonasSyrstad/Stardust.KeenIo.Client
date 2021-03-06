﻿#region license header
//
// KeenClient.cs
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
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.Query;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client
{
    public static class KeenClient
    {
        static KeenClient()
        {
            if (!AdjustConnectionLimit) return;
            var connectionLimit = Environment.ProcessorCount * 48;
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["stardust:defaultConnectionLimit"]))
            {
                int value;
                if (int.TryParse(ConfigurationManager.AppSettings["stardust:defaultConnectionLimit"], out value)) connectionLimit = value;
            }
            ServicePointManager.DefaultConnectionLimit = connectionLimit;
        }

        private static bool AdjustConnectionLimit => ServicePointManager.DefaultConnectionLimit > (Environment.ProcessorCount * 8) && string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["stardust:defaultConnectionLimit"]);

        internal static string baseUrl = "https://api.keen.io";

        internal static string projectId = "";


        public static void SetBaseUrl(string url) => baseUrl = url;

        /// <summary>
        /// Set to false in order to retrhow exceptions. Default value= true.
        /// </summary>
        public static bool SwallowException { get; private set; } = true;

        public static void Initialize(this KeenConfiguration configuration)
        {

            if (configuration == null) throw new ArgumentNullException(nameof(configuration), "configuration object is null");
            if (configuration == null) throw new ArgumentNullException(nameof(configuration.ProjectId), "configuration.ProjectId object is null");
            if (!string.IsNullOrWhiteSpace(configuration.BaseUrl))
                SetBaseUrl(configuration.BaseUrl);
            SwallowException = configuration.SwalowException;
            SetProjectId(configuration.ProjectId);
            SetReaderKey(configuration.ReaderKey);
            SetWriterKey(configuration.WriterKey);
            if (configuration.GlobalProperties == null) return;
            foreach (var globalProperty in configuration.GlobalProperties)
            {
                SetGlobalProperty(globalProperty.Key, globalProperty.Value);
            }
            if (configuration.BatchSize.HasValue && configuration.BatchSize.Value > 10)
                KeenBatchClient.SetBatchSize(configuration.BatchSize.Value);
        }

        public static void SetProjectId(string project) => projectId = project;

        public static void SetReaderKey(string key) => KeenGlobalConfig.SetReaderKey(key);

        public static void SetWriterKey(string key) => KeenGlobalConfig.SetWriterKey(key);

        public static void AddEvent(string collection, dynamic value) => AddEventAsync(collection, value);

        public static async Task AddEventAsync(string collection, dynamic value)
        {
            try
            {
                await Collector.AddEvent(projectId, collection, value);
            }
            catch (Exception)
            {
                if (!SwallowException)
                    throw;
            }
        }


        public static void AddEvents(IDictionary<string, IEnumerable<object>> items) => AddEventsAsync(items);

        public static async Task AddEventsAsync(IDictionary<string, IEnumerable<object>> items)
        {
            try
            {
                await Collector.AddEvents(projectId, items);
            }
            catch (Exception)
            {
                if (!SwallowException)
                    throw;
            }
        }

        public static async Task AddEventsAsync(string collection, IEnumerable<object> items) => await AddEventsAsync(new Dictionary<string, IEnumerable<object>> { { collection, items } });

        public static void AddEvents(string collection, IEnumerable<object> items) => AddEventsAsync(new Dictionary<string, IEnumerable<object>> { { collection, items } });

        public static async Task<CollectionInfo> GetCollectionAsync(string collection) => await KeenInspector.GetCollectionAsync(projectId, collection);

        internal static IKeenInspection KeenInspector => ProxyFactory.CreateInstance<IKeenInspection>(baseUrl);

        public static CollectionInfo GetCollection(string collection) => KeenInspector.GetCollection(projectId, collection);

        public static async Task<IEnumerable<CollectionInfo>> GetCollectionsAsync() => await KeenInspector.GetAllCollectionAsync(projectId);

        public static IEnumerable<CollectionInfo> GetCollections() => KeenInspector.GetAllCollection(projectId);

        public static async Task<dynamic> QueryAsync(this QueryType queryType, QueryBody query) => await KeenInspector.QueryAsync(projectId, queryType, query);

        public static dynamic Query(this QueryType queryType, QueryBody query) => KeenInspector.Query(projectId, queryType, query);

        public static IEventCollector SetGlobalProperty(string name, object value) => Collector.SetGlobalProperty(name, value);

        private static IEventCollector Collector => ProxyFactory.CreateInstance<IEventCollector>(baseUrl);
    }
}
