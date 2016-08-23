using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class CachedQueries
    {
        [JsonProperty("allowed", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Allowed { get; set; }

        [JsonProperty("blocked", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Blocked { get; set; }
    }
}