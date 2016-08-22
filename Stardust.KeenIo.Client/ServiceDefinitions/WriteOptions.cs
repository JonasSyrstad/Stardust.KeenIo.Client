using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class WriteOptions
    {
        [JsonProperty("autofill")]
        public Dictionary<string , object> Autofill { get; set; }
    }
}