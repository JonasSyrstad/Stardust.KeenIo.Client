using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    public class FilterPart
    {
        [JsonProperty("property_name")]
        public string PropertyName { get; set; }

        [JsonProperty("operator")]
        public OperatorType Operator { get; set; }

        [JsonProperty("property_value")]
        public object PropertyValue { get; set; }
    }
}