using Newtonsoft.Json;
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class SavedQueries:CachedQueries
    {

        [JsonProperty("filters")]
        public Filter[] Filters { get; set; }
    }
}