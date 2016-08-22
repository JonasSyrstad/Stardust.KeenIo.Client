using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.UserAgent;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    [KeenMasterAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0")]
    public interface IKeenProjectManagement
    {
        [HttpGet]
        [Route("projects")]
        ProjectInfo[] GetProjects();

        [HttpGet]
        [Route("projects")]
        Task<ProjectInfo[]> GetProjectsAsync();

        [HttpGet]
        [Route("projects/{projectId}")]
        ProjectInfo GetProject([In(InclutionTypes.Path)] string projectId);

        [HttpGet]
        [Route("projects/{projectId}")]
        Task<ProjectInfo> GetProjectAsync([In(InclutionTypes.Path)] string projectId);

        [HttpPost]
        [Route("projects/{projectId}/keys")]
        ApiKeyDescription CreateApiKey([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpPost]
        [Route("projects/{projectId}/keys")]
        Task<ApiKeyDescription> CreateApiKeyAsync([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpGet]
        [Route("projects/{projectId}/keys")]
        ApiKeyDescription[] GetCustomKeys([In(InclutionTypes.Path)]string projectId);

        [HttpGet]
        [Route("projects/{projectId}/keys")]
        Task<ApiKeyDescription[]> GetCustomKeysAsync([In(InclutionTypes.Path)]string projectId);

        [HttpGet]
        [Route("projects/{projectId}/keys/{customKey}")]
        ApiKeyDescription GetCustomKey([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpGet]
        [Route("projects/{projectId}/keys/{customKey}")]
        Task<ApiKeyDescription> GetCustomKeyAsync([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}")]
        ApiKeyDescriptionRequest UpdateApiKey([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}")]
        Task<ApiKeyDescriptionRequest> UpdateApiKeyAsync([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/revoke")]
        void RevokeCustomKey([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/revoke")]
        Task RevokeCustomKeyAsync([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/unrevoke")]
        void UnRevokeCustomKey([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/unrevoke")]
        Task UnRevokeCustomKeyAsync([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);
    }
}