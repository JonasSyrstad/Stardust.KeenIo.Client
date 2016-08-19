﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.Query;
using Stardust.KeenIo.Client.ServiceDefinitions;
using Xunit;

namespace Stardust.KeenIo.Client.Tests
{

    public class ClientTests
    {

        [Fact]
        public async Task Initialization()
        {
            KeenClient.Initialize(new KeenConfiguration("560c2d6e672e6c1204fba8d5")
            {
                GlobalProperties = new Dictionary<string, object>
                {
                { "host", Environment.MachineName },
                { "user", Environment.MachineName }
                }

            });
            await KeenClient.AddEventAsync("init", new { Message = "Initialization" });
        }

        [Fact]
        public async Task AddEntryTest()
        {
            var client = ProxyFactory.CreateInstance<IEventCollector>("https://api.keen.io");
            client.SetGlobalProperty("host", Environment.MachineName).SetGlobalProperty("user", Environment.UserName);

            await client.AddEvent("560c2d6e672e6c1204fba8d5", "test", new { TimeStamp = DateTime.UtcNow, Name = "UnitTest" });

        }

        [Fact]
        public async Task AddEntriesBatchTest()
        {
            var client = ProxyFactory.CreateInstance<IEventCollector>("https://api.keen.io");
            client.SetGlobalProperty("host", Environment.MachineName).SetGlobalProperty("user", Environment.UserName);
            var envents = new Dictionary<string, IEnumerable<object>>
                              {
                                  {
                                      "collection1",
                                      new List<object>
                                          {
                                              new { TimeStamp = DateTime.UtcNow, Name = "UnitTest2" },
                                              new { TimeStamp = DateTime.UtcNow, Name = "UnitTest3" },
                                              new { TimeStamp = DateTime.UtcNow, Name = "UnitTest4" }
                                          }
                                  },
                                  {
                                      "collection2",
                                      new List<object>
                                          {
                                              new { TimeStamp2 = DateTime.UtcNow, Name2 = "UnitTest2" },
                                              new { TimeStamp2 = DateTime.UtcNow, Name2 = "UnitTest3" },
                                              new { TimeStamp2 = DateTime.UtcNow, Name2 = "UnitTest4" }
                                          }
                                  }
                              };
            var m = JObject.FromObject(envents);
            var m2 = JObject.FromObject(new { TimeStamp2 = DateTime.UtcNow, Name2 = "UnitTest4" });
            await client.AddEvents("560c2d6e672e6c1204fba8d5", envents);

        }

        [Fact]
        public void GetCollection()
        {
            var reader = ProxyFactory.CreateInstance<IKeenInspection>("https://api.keen.io");
            var all = reader.GetAllCollection("560c2d6e672e6c1204fba8d5");
            Assert.NotEmpty(all);
            foreach (var collection in all)
            {
                var c = reader.GetCollection("560c2d6e672e6c1204fba8d5", collection.Name);
                Assert.NotNull(c);
            }
        }

        [Fact]
        public void QueryTest()
        {
            var reader = ProxyFactory.CreateInstance<IKeenInspection>("https://api.keen.io");
            var msg = JsonConvert.SerializeObject(new QueryBody { TimeFrame = TimeFrame.ThisWeek, Timezone = Timezone.EuropeStockholm, EventCollection = "collection2" });

            Assert.NotNull(msg);
            var result = reader.Query(
                "560c2d6e672e6c1204fba8d5",
                QueryType.Count,
                new QueryBody
                {
                    TimeFrame = TimeFrame.ThisWeek,
                    Timezone = Timezone.EuropeStockholm,
                    EventCollection = "collection2",
                    //GroupBy = "Name2"
                });
            Assert.NotNull(result);
            var cnt = result.result;
            Assert.NotNull(cnt);
        }

        [Fact]
        public async Task QueryExtensionsTests()
        {
            KeenClient.Initialize(new KeenConfiguration("560c2d6e672e6c1204fba8d5")
            {
                GlobalProperties = new Dictionary<string, object>
                {
                    { "host", Environment.MachineName },
                    { "user", Environment.MachineName }
                }

            });
            var result = await QueryType.Extraction.QueryAsync(new QueryBody
            {
                EventCollection = "collection2",
                Timezone = Timezone.EuropeStockholm,
                TimeFrame = TimeFrame.ThisNWeeks(2)
            });
            Assert.NotNull(result);

        }


        [Fact]
        public void CountAllEventsTest()
        {
            KeenClient.Initialize(new KeenConfiguration("560c2d6e672e6c1204fba8d5")
            {
                GlobalProperties = new Dictionary<string, object>
                {
                    { "host", Environment.MachineName },
                    { "user", Environment.MachineName }
                }

            });

            var cnt = KeenClient.GetCollections().GetEventCount(TimeFrame.ThisWeek);
            Assert.True(cnt > 0);
        }

        [Fact]
        public async Task ValueFetcherTest()
        {
            KeenClient.Initialize(
                new KeenConfiguration("560c2d6e672e6c1204fba8d5")
                    {
                        GlobalProperties =
                            new Dictionary<string, object>
                                {
                                    { "host", Environment.MachineName },
                                    { "user", Environment.MachineName },
                                    { "fetchedValue", new ScopedValueFetcher { FetchAction = () => Environment.OSVersion } },
                                    { "time", new ScopedValueFetcher { FetchAction = () => DateTime.UtcNow.Ticks } }
                                }
                    });
            await KeenClient.AddEventAsync("fetcherTest", new { TimeStamp = DateTime.UtcNow, Name = "UnitTest" });
        }
    }
}
