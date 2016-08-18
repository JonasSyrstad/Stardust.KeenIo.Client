using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
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
}