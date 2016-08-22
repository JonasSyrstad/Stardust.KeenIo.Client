using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ApiKeyOptions
    {
        [JsonProperty("queries")]
        public QueryOptions Queries { get; set; }
        [JsonProperty("cached_queries")]
        public CachedQueries CachedQueries { get; set; }

        [JsonProperty("saved_queries")]
        public SavedQueries SavedQueries { get; set; }

        [JsonProperty("writes")]
        public WriteOptions Writes { get; set; }
    }
}