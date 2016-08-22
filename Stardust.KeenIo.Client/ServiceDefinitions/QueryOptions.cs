using Newtonsoft.Json;
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class QueryOptions
    {
        [JsonProperty("filters")]
        public Filter[] Filters { get; set; }
    }
}