using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Annotations.UserAgent;

namespace Stardust.KeenIo.Client
{
    [KeenReaderAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    public interface IEventCollections
    {
        [Route("{projectId}/events/{collectionName}")]
        Task<CollectionInfo> GetCollectionAsync([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName);

        [Route("{projectId}/events/{collectionName}")]
        CollectionInfo GetCollection([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName);

        [Route("{projectId}/events")]
        Task<IEnumerable<CollectionInfo>> GetAllCollectionAsync([In(InclutionTypes.Path)] string projectId);

        [Route("{projectId}/events")]
        IEnumerable<CollectionInfo> GetAllCollection([In(InclutionTypes.Path)] string projectId);


    }

    public class CollectionInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string,string> Properties { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    [KeenWriteAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    public interface IEventCollector : IServiceWithGlobalParameters
    {
        [HttpPost]
        [Route("{projectId}/events/{collectionName}")]
        Task AddEvent([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName, [In(InclutionTypes.Body)] object eventEntry);

        //[HttpPost]
        //[Route("{projectId}/events/{collectionName}")]
        //Task AddEvents([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName, [ExtensionLevel(1)][In(InclutionTypes.Body)] IEnumerable<object> eventEntries);

        [HttpPost]
        [Route("{projectId}/events")]
        Task AddEvents([In(InclutionTypes.Path)]string projectId, [ExtensionLevel(3)][In(InclutionTypes.Body)]IDictionary<string, IEnumerable<object>> eventEntry);
    }
}