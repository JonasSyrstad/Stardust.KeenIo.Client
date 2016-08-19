# Stardust.KeenIo.Client
A .net client for keen.io based on Stardust.Interstellar.Rest. Serves as a demo on how to easily build .net clients for any rest api with little effort.

get on nuget: Install-Package Stardust.KeenIo.Client

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
KeenClient.Initialize(new KeenConfiguration(ProjectIdFromKeenIo)
            {
                GlobalProperties = new Dictionary<string, object>
                {
                { "host", Environment.MachineName },
                { "user", Environment.MachineName }
                }
            });
```


To add a new event in keen.io:
```CS
KeenClient.AddEventAsync(string collectionName,new {Name="Test", Message="This is a test message"});//note that its not awaited. this acts as a fire and forget type non blocking call to keen.io
```

To Execute queries against keen.io:

```CS
var result=QueryType.Average.Query(new QueryBody{ TimeFrame = TimeFrame.ThisWeek, Timezone = Timezone.EuropeStockholm, EventCollection = "collection2" ,GroupBy = "Name2" ,TargetProperty="TimeStamp2"}});
foreach(dynamic resultPart in result)//note that the result is a dynamic, you need to poke at it to find the data structure
{
  //do stuff.
}
```

