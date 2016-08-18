using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    

    public class QueryBody : DynamicObject
    {
        private Dictionary<string, object> dynamicMembers = new Dictionary<string, object>();
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dynamicMembers.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            try
            {
                if (dynamicMembers.ContainsKey(binder.Name)) dynamicMembers[binder.Name] = value;
                else
                    dynamicMembers.Add(binder.Name, value);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return dynamicMembers.Keys;
        }

        [JsonProperty("event_collection")]
        public string EventCollection { get; set; }

        [JsonProperty("timeframe", ItemConverterType = typeof(ToStringSerializer))]
        public TimeFrame TimeFrame { get; set; }

        [JsonProperty("group_by")]
        public string GroupBy { get; set; }

        [JsonProperty("timezone", ItemConverterType = typeof(ToStringSerializer))]
        public Timezone Timezone { get; set; }


        [JsonProperty("target_property")]
        public string TargetProperty { get; set; }


    }
}