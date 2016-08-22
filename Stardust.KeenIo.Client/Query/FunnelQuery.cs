using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    public class FunnelQuery
    {
        [JsonProperty("steps")]
        public List<FunnelStep> Steps { get; set; }
    }
}