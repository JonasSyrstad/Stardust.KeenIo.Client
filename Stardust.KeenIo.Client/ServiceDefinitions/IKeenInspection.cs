using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.UserAgent;
using Stardust.Interstellar.Rest.Service;
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    [KeenReaderAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    [ErrorHandler(typeof(ErrorHandling.KeenErrorHandler))]
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

        [HttpPost]
        [Route("{projectId}/queries/multi_analysis")]
        dynamic MultiQuery([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Body)] MultiQuery query);

        [HttpPost]
        [Route("{projectId}/queries/multi_analysis")]
        Task<dynamic> MultiQueryAsync([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Body)] MultiQuery query);

        [HttpPost]
        [Route("{projectId}/queries/funnel")]
        dynamic Funnel([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Body)] FunnelQuery query);

        [HttpPost]
        [Route("{projectId}/queries/funnel")]
        Task<dynamic> FunnelAsync([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Body)] FunnelQuery query);
    }
}