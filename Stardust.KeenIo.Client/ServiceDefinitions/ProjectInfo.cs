using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ProjectInfo
    {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("events_url")]
        public string EventsUrl { get; set; }

        [JsonProperty("events")]
        public IEnumerable<EventCollection> EventCollections { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }
        [JsonProperty("partners_url")]
        public string PartnersUrl { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("queries_url")]
        public string QueriesUrl { get; set; }
    }
}