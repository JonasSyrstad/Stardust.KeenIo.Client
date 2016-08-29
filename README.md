# Stardust.KeenIo.Client
A .net client for keen.io based on Stardust.Interstellar.Rest. Serves as a demo on how to easily build .net clients for any rest api with little effort.

get on nuget: Install-Package Stardust.KeenIo.Client

**Initialization**

On application start:
```CS
KeenClient.SetProjectId(ProjectIdFromKeenIo);
KeenClient.SetGlobalProperty("machineName",Environment.Machine).SetGlobalProperty("userName",Environment.UserName);
//Optional set programatically or in config
KeenClient.SetReaderKey("readerKeyFromKeenIo");//appSettings key:keen:readerKey
KeenClient.SetWriterKey("writerKeyFromKeenIo");//appSettings key:keen:writerKey
```
or
```CS
KeenClient.Initialize(
                new KeenConfiguration(ProjectIdFromKeenIo)
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
```
**Adding events to keen.io**

To add a new event in keen.io:
```CS
KeenClient.AddEventAsync("collectionName",new {Name="Test", Message="This is a test message"});//note that its not awaited. this acts as a fire and forget type non blocking call to keen.io
```
*NEW in 1.5*
Batch processor: Add events to keen.io in batches. Reduce the number of http calls to keen.io servers

initialization
```CS
KeenClient.Initialize(new KeenConfiguration { ProjectId = ProjectIdFromKeenIo, BatchSize = 10 });
KeenBatchClient.StartEventPump();
```

add an event
```CS
KeenBatchClient.AddEvent(collectionName, SomeEventToPost);
```
To ensure that all events has been written to keen.io you have to call the following at application shutdown.
```CS
KeenBatchClient.ShutdownEventPump(); //if your shutdown is async there is also an async version of this method.
```

**Analysis**

To Execute queries against keen.io:

```CS
var result=QueryType.Average.Query(new QueryBody{ TimeFrame = TimeFrame.ThisWeek, Timezone = Timezone.EuropeStockholm, EventCollection = "collection2" ,GroupBy = "Name2" ,TargetProperty="TimeStamp2"}});
foreach(dynamic resultPart in result)//note that the result is a dynamic, you need to poke at it to find the data structure
{
  //do stuff.
}
```

Execute a funnel analysis:
```C

 KeenClient.Initialize(new KeenConfiguration(ProjectIdFromKeenIo));
            var query = new FunnelQuery
                            {
                                Steps =
                                    new List<FunnelStep>
                                        {
                                            new FunnelStep { ActorProperty = "Name", EventCollection = "collection1", TimeFrame = TimeFrame.ThisWeek},
                                            new FunnelStep { ActorProperty = "Name2", EventCollection = "collection2", TimeFrame = TimeFrame.ThisWeek }
                                        }
                            };
            var result = await query.FunnelAsync();
```


