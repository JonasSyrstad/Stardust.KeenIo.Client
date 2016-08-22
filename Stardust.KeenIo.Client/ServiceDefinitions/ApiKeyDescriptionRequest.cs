using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ApiKeyDescriptionRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }

        [JsonProperty("permitted")]
        public string[] Permitted { get; set; }

        [JsonProperty("options")]
        public ApiKeyOptions Options { get; set; }
    }
}