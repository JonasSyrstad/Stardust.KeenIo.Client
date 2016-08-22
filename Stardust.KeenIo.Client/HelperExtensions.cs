using System.Collections.Generic;
using System.Threading.Tasks;
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client
{
    public static class HelperExtensions
    {
        public static long GetEventCount(this IEnumerable<CollectionInfo> collections, TimeFrame timeFrame)
        {
            long totalEvents = 0;
            foreach (var collectionInfo in collections)
            {
                var result = collectionInfo.GetEventCount( timeFrame);
                totalEvents +=result ;
            }
            return totalEvents;
        }

        public static long GetEventCount(this CollectionInfo collectionInfo,TimeFrame timeFrame)
        {
            var result = QueryType.Count.Query(new QueryBody { EventCollection = collectionInfo.Name, TimeFrame = timeFrame, Timezone = Timezone.UTC });
            return (long)result.result;
        }

        public static async Task<long> GetEventCountAsync(this IEnumerable<CollectionInfo> collections, TimeFrame timeFrame)
        {
            long totalEvents = 0;
            foreach (var collectionInfo in collections)
            {
                var result = await collectionInfo.GetEventCountAsync(timeFrame);
                totalEvents += result;
            }
            return totalEvents;
        }

        public static async Task<long> GetEventCountAsync(this CollectionInfo collectionInfo,TimeFrame timeFrame )
        {
            var result = await QueryType.Count.QueryAsync(new QueryBody { EventCollection = collectionInfo.Name, TimeFrame = timeFrame, Timezone = Timezone.UTC });
            return (long)result.result; ;
        }

        
    }
}