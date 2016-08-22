using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class KeenUser
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}