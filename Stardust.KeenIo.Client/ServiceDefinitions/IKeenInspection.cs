using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.UserAgent;
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    [KeenReaderAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    public interface IKeenInspection
    {
        [Route("{projectId}/events/{collectionName}")]
        Task<CollectionInfo> GetCollectionAsync([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName);

        [Route("{projectId}/events/{collectionName}")]
        CollectionInfo GetCollection([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName);

        [Route("{projectId}/events")]
        Task<IEnumerable<CollectionInfo>> GetAllCollectionAsync([In(InclutionTypes.Path)] string projectId);

        [Route("{projectId}/events")]
        IEnumerable<CollectionInfo> GetAllCollection([In(InclutionTypes.Path)] string projectId);

        [HttpPost]
        [Route("{projectId}/queries/{query}")]
        Task<dynamic> QueryAsync([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] QueryType query, [In(InclutionTypes.Body)]QueryBody body);

        [HttpPost]
        [Route("{projectId}/queries/{query}")]
        dynamic Query([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] QueryType query, [In(InclutionTypes.Body)]QueryBody body);
    }
}