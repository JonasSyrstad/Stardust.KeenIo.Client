using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.Management;
using Stardust.KeenIo.Client.Query;
using Stardust.KeenIo.Client.ServiceDefinitions;
using Xunit;

namespace Stardust.KeenIo.Client.Tests
{

    public class ClientTests
    {

        [Fact]
        public async Task aInitialization()
        {
            new KeenConfiguration("57bb35408db53dfda8a6cc37")
                {
                    GlobalProperties = new Dictionary<string, object>
                                           {
                                               { "host", Environment.MachineName },
                                               { "user", Environment.MachineName }
                                           }

                }.Initialize();
            await KeenClient.AddEventAsync("init", new { Message = "Initialization" });
        }

        [Fact]
        public async Task AddEntryTest()
        {
            var client = ProxyFactory.CreateInstance<IEventCollector>("https://api.keen.io");
            client.SetGlobalProperty("host", Environment.MachineName).SetGlobalProperty("user", Environment.UserName);

            await client.AddEvent("57bb35408db53dfda8a6cc37", "test", new { TimeStamp = DateTime.UtcNow, Name = "UnitTest" });

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
                                              new { TimeStamp2 = DateTime.UtcNow, Name2 = "UnitTest44" }
                                          }
                                  }
                              };
            var m = JObject.FromObject(envents);
            var m2 = JObject.FromObject(new { TimeStamp2 = DateTime.UtcNow, Name2 = "UnitTest4" });
            await client.AddEvents("57bb35408db53dfda8a6cc37", envents);

        }

        [Fact]
        public async Task FunnelTest()
        {
            new KeenConfiguration("57bb35408db53dfda8a6cc37")
                {
                    GlobalProperties = new Dictionary<string, object>
                                           {
                                               { "host", Environment.MachineName },
                                               { "user", Environment.MachineName }
                                           }

                }.Initialize();
            var query = new FunnelQuery
                            {
                                Steps =
                                    new List<FunnelStep>
                                        {
                                            new FunnelStep { ActorProperty = "Name", EventCollection = "collection1", TimeFrame = TimeFrame.ThisWeek},
                                            new FunnelStep { ActorProperty = "Name2", EventCollection = "collection2", TimeFrame = TimeFrame.ThisWeek }
                                        }
                            };
            //var msg = JsonConvert.SerializeObject(query);
            //Assert.NotEmpty(msg);
            var result = await query.FunnelAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetCollection()
        {
            var reader = ProxyFactory.CreateInstance<IKeenInspection>("https://api.keen.io");
            var all = reader.GetAllCollection("57bb35408db53dfda8a6cc37");
            Assert.NotEmpty(all);
            foreach (var collection in all)
            {
                var c = reader.GetCollection("57bb35408db53dfda8a6cc37", collection.Name);
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
                "57bb35408db53dfda8a6cc37",
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
            new KeenConfiguration("57bb35408db53dfda8a6cc37")
                {
                    GlobalProperties = new Dictionary<string, object>
                                           {
                                               { "host", Environment.MachineName },
                                               { "user", Environment.MachineName }
                                           }

                }.Initialize();
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
            KeenClient.Initialize(new KeenConfiguration("57bb35408db53dfda8a6cc37")
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
            new KeenConfiguration("57bb35408db53dfda8a6cc37")
                {
                    GlobalProperties = new Dictionary<string, object>
                                           {
                                               { "host", Environment.MachineName },
                                               { "user", Environment.MachineName },
                                               { "fetchedValue", new ScopedValueFetcher { FetchAction = () => Environment.OSVersion } },
                                               { "time", new ScopedValueFetcher { FetchAction = () => DateTime.UtcNow.Ticks } }
                                           }
                }.Initialize();
            await KeenClient.AddEventAsync("fetcherTest", new { TimeStamp = DateTime.UtcNow, Name = "UnitTest" });
        }

        [Fact]
        public void CreateApiKeyTest()
        {
            new KeenConfiguration("57bb35408db53dfda8a6cc37").Initialize();
            var client = new ProjectManagementClient("dummy");
           var key= client.CreateApiKey(new ApiKeyDescriptionRequest {Name = "TEST"+DateTime.UtcNow.Ticks,IsActive = true});
            client.RevokeApiKey(key.Key);
            Assert.NotNull(key);
        }
    }
}
