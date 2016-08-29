using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.UserAgent;
using Stardust.Interstellar.Rest.Service;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    [KeenOrganizationAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0")]
    [ErrorHandler(typeof(ErrorHandling.KeenErrorHandler))]
    public interface IKeenAdministration
    {
        [HttpGet]
        [Route("organizations/{organizationId}/projects/{projectId}")]
        ProjectManagementInfo GetProject([In(InclutionTypes.Path)]string organizationId, [In(InclutionTypes.Path)]string projectId);

        [HttpGet]
        [Route("organizations/{organizationId}/projects/{projectId}")]
        Task<ProjectManagementInfo> GetProjectAsync([In(InclutionTypes.Path)]string organizationId, [In(InclutionTypes.Path)]string projectId);

        [HttpPost]
        [Route("organizations/{organizationId}/projects")]
        ProjectManagementInfo CreateProject([In(InclutionTypes.Path)]string organizationId,ProjectManagementInfoBase project);

        [HttpPost]
        [Route("organizations/{organizationId}/projects")]
        Task<ProjectManagementInfo> CreateProjectAsync([In(InclutionTypes.Path)]string organizationId, ProjectManagementInfoBase project);
    }
}