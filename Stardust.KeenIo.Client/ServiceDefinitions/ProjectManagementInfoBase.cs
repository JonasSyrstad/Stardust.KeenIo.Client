using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ProjectManagementInfoBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("users")]
        public KeenUser[] Users { get; set; }

        [JsonProperty("preferences")]
        public Preferences Preferences { get; set; }
    }
}