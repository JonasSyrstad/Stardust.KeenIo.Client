using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client.Management
{
    public static class ProjectManagementExtensions
    {
        public static ProjectManagementInfo CreateProject(this ManagementClient client, ProjectManagementInfoBase project) => client.GetClient().CreateProject(client.organizationId, project);
        public static async Task<ProjectManagementInfo> CreateProjectAsync(this ManagementClient client, ProjectManagementInfoBase project) => await client.GetClient().CreateProjectAsync(client.organizationId, project);
        
        public static ProjectManagementInfo GetProject(this ManagementClient client, string project) => client.GetClient().GetProject(client.organizationId, project);
        public static async Task<ProjectManagementInfo> GetProjectAsync(this ManagementClient client, string project) => await client.GetClient().GetProjectAsync(client.organizationId, project);

        public static ProjectInfo[] GetProjects(this ManagementClient client)
        {
            return ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProjects();
        }

        public static async Task<ProjectInfo[]> GetProjectsAsync(this ManagementClient client)
        {
            return  await ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProjectsAsync();
        }

        public static ProjectInfo GetProjectMetadata(this ManagementClient client, string projectId)
        {
            return ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProject(projectId);
        }

        public static async Task<ProjectInfo> GetProjectMetadataAsync(this ManagementClient client, string projectId)
        {
            return await ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProjectAsync(projectId);
        }
    }
}