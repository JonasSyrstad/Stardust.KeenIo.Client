using System.Threading.Tasks;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client.Management
{
    public static class ApiKeyExtensions
    {
        public static ApiKeyDescription GetApiKey(this ProjectManagementClient client, string apiKey) => client.GetManagementClient().GetCustomKey(KeenClient.projectId, apiKey);

        public static async Task<ApiKeyDescription> GetApiKeyAsync(this ProjectManagementClient client, string apiKey) =>await  client.GetManagementClient().GetCustomKeyAsync(KeenClient.projectId, apiKey);

        public static ApiKeyDescription[] GetApiKeys(this ProjectManagementClient client) => client.GetManagementClient().GetCustomKeys(KeenClient.projectId);

        public static async Task<ApiKeyDescription[]> GetApiKeysAsync(this ProjectManagementClient client) => await client.GetManagementClient().GetCustomKeysAsync(KeenClient.projectId);

        public static ApiKeyDescription CreateApiKey(this ProjectManagementClient client, ApiKeyDescriptionRequest apiKey) => client.GetManagementClient().CreateApiKey(KeenClient.projectId, apiKey);

        public static async Task<ApiKeyDescription> CreateApiKeyAsync(this ProjectManagementClient client, ApiKeyDescriptionRequest apiKey) => await client.GetManagementClient().CreateApiKeyAsync(KeenClient.projectId, apiKey);


        public static ApiKeyDescriptionRequest UpdateApiKey(this ProjectManagementClient client, ApiKeyDescriptionRequest apiKey) => client.GetManagementClient().UpdateApiKey(KeenClient.projectId, apiKey);

        public static async Task<ApiKeyDescriptionRequest> UpdateApiKeyAsync(this ProjectManagementClient client, ApiKeyDescriptionRequest apiKey) => await client.GetManagementClient().UpdateApiKeyAsync(KeenClient.projectId, apiKey);

        public static void UnrevokeApiKey(this ProjectManagementClient client, string apiKey) => client.GetManagementClient().UnRevokeCustomKey(KeenClient.projectId, apiKey);

        public static async Task UnrevokeApiKeyAsync(this ProjectManagementClient client, string apiKey) => await client.GetManagementClient().RevokeCustomKeyAsync(KeenClient.projectId, apiKey);

        public static void RevokeApiKey(this ProjectManagementClient client, string apiKey) => client.GetManagementClient().UnRevokeCustomKey(KeenClient.projectId, apiKey);

        public static async Task RevokeApiKeyAsync(this ProjectManagementClient client, string apiKey) => await client.GetManagementClient().RevokeCustomKeyAsync(KeenClient.projectId, apiKey);
    }
}