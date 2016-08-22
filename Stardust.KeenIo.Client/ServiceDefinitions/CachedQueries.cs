using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class CachedQueries
    {
        [JsonProperty("allowed")]
        public string[] Allowed { get; set; }

        [JsonProperty("blocked")]
        public string[] Blocked { get; set; }
    }
}