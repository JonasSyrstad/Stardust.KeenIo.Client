using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client.Management
{
    public class ProjectManagementClient
    {
        public ProjectManagementClient(string organizationId)
        {
            
        }

        internal IKeenProjectManagement GetManagementClient()
        {
            return ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl);
        }
    }
}