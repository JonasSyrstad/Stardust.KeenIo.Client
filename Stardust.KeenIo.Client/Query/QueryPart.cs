using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    internal class QueryPart
    {
        [JsonProperty("analysis_type")]
        public QueryType AnalysisType { get; set; }

        [JsonProperty("target_property")]
        public string TargetProperty { get; set; }
    }
}