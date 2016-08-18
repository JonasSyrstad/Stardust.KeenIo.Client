using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client
{
    public class CollectionInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string,string> Properties { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}