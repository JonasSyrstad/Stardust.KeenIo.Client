using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ApiKeyOptions
    {
        [JsonProperty("queries", NullValueHandling = NullValueHandling.Ignore)]
        public QueryOptions Queries { get; set; }
        [JsonProperty("cached_queries", NullValueHandling = NullValueHandling.Ignore)]
        public CachedQueries CachedQueries { get; set; }

        [JsonProperty("saved_queries", NullValueHandling = NullValueHandling.Ignore)]
        public SavedQueries SavedQueries { get; set; }

        [JsonProperty("writes", NullValueHandling = NullValueHandling.Ignore)]
        public WriteOptions Writes { get; set; }
    }
}