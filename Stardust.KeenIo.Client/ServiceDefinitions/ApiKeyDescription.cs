using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ApiKeyDescription: ApiKeyDescriptionRequest
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}