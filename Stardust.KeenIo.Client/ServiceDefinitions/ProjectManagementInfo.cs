using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ProjectManagementInfo : ProjectManagementInfoBase
    {
        [JsonProperty("apiKeys")]
        public ApiKeys ApiKeys { get; set; }
    }

    public class ApiKeys
    {
        [JsonProperty("masterKey")]
        public string Master { get; set; }
        [JsonProperty("readKey")]
        public string Read { get; set; }

        [JsonProperty("writeKey")]
        public string Write { get; set; }
    }
}