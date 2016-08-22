using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ProjectManagementInfo : ProjectManagementInfoBase
    {
        [JsonProperty("api_keys")]
        public Dictionary<string,string> ApiKeys { get; set; }
    }
}