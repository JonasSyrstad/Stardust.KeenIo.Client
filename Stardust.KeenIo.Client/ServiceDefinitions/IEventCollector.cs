using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Annotations.UserAgent;
using Stardust.Interstellar.Rest.Service;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    [KeenWriteAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    [ErrorHandler(typeof(ErrorHandling.KeenErrorHandler))]
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