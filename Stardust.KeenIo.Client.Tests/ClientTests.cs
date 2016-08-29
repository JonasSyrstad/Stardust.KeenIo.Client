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
using Xunit.Abstractions;

namespace Stardust.KeenIo.Client.Tests
{
    public class ClientTests
    {

        [Fact]
        public async Task aInitialization()
        {
            new KeenConfiguration("560c2d6e672e6c1204fba8d5")
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

            await client.AddEvent("560c2d6e672e6c1204fba8d5", "test", new { TimeStamp = DateTime.UtcNow, Name = "UnitTest" });

        }

        private readonly ITestOutputHelper output;

        public ClientTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task AddEntriesBatchTest()
        {
            new KeenConfiguration {SwalowException = false}.Initialize();
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
            await client.AddEvents("560c2d6e672e6c1204fba8d5", envents);

        }

        [Fact]
        public async Task FunnelTest()
        {
            new KeenConfiguration("560c2d6e672e6c1204fba8d5")
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
            new KeenConfiguration("560c2d6e672e6c1204fba8d5")
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
            new KeenConfiguration("560c2d6e672e6c1204fba8d5")
            {
                GlobalProperties = new Dictionary<string, object>
                                           {
                                               { "host", Environment.MachineName },
                                               { "user", Environment.UserName },
                                               { "fetchedValue", new ScopedValueFetcher { FetchAction = () => Environment.OSVersion } },
                                               { "time", new ScopedValueFetcher { FetchAction = () => DateTime.UtcNow.Ticks } }
                                           }
            }.Initialize();
            await KeenClient.AddEventAsync("fetcherTest", new { TimeStamp = DateTime.UtcNow, Name = "UnitTest" });
        }

        [Fact]
        public void CreateApiKeyTest()
        {
            try
            {
                new KeenConfiguration("560c2d6e672e6c1204fba8d5").Initialize();
                var client = new ProjectManagementClient("57bb18a707271955bac4b202");
                var msg = new ApiKeyDescriptionRequest
                {
                    Name = "TEST" + DateTime.UtcNow.Ticks,
                    IsActive = true,
                    Options = new ApiKeyOptions { Writes = new WriteOptions { Autofill = new Dictionary<string, object> { { "customerid", 1234 } } } },
                    Permitted = new[] { ApiKeyPermission.Write }
                };
                var stringmsg = JsonConvert.SerializeObject(msg);
                Assert.NotEmpty(stringmsg);
                var key = client.CreateApiKey(msg);
                client.RevokeApiKey(key.Key);
                Assert.NotNull(key);
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.Message);
                throw;
            }
        }

        [Fact]
        public void CreateProjectTest()
        {
            new KeenConfiguration("560c2d6e672e6c1204fba8d5").Initialize();
            var client = new ManagementClient("57bb18a707271955bac4b202");
            var result = client.CreateProject(
                 new ProjectManagementInfoBase
                 {
                     Name = "UnitTestPrj_" + DateTime.UtcNow.Ticks,
                     Users = new[] { new KeenUser { Email = "jsyrstad2@gmail.com" } },
                     Preferences = new Preferences { S3BucketName = "test" }
                 });
            Assert.NotNull(result);
            Assert.NotNull(result.ApiKeys);
        }

        [Fact]
        public void GetProjectTest()
        {
            new KeenConfiguration("560c2d6e672e6c1204fba8d5").Initialize();
            var client = new ManagementClient("57bb18a707271955bac4b202");
            var result = client.GetProject("560c2d6e672e6c1204fba8d5");
            var data = JsonConvert.SerializeObject(result);
            Assert.NotEmpty(data);
            Assert.NotNull(result);
            Assert.NotNull(result.ApiKeys);
        }
    }
}
