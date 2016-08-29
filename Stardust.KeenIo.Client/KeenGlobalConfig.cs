using Newtonsoft.Json;
using Stardust.KeenIo.Client.ServiceDefinitions;
using Stardust.Interstellar.Rest.Common;

namespace Stardust.KeenIo.Client
{
    public static class KeenGlobalConfig
    {
        static KeenGlobalConfig()
        {
            new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.None,
                    
                }.AddClientSerializer<IKeenInspection>()
                .AddClientSerializer<IEventCollector>()
                .AddClientSerializer<IKeenAdministration>()
                .AddClientSerializer<IKeenProjectManagement>();
        }

        public static void SetWriterKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }

        public static void SetReaderKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }
    }
}