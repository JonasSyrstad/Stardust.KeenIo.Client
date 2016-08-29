using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client.Management
{
    public class ManagementClient
    {
        internal readonly string organizationId;

        public ManagementClient(string organizationId)
        {
            this.organizationId = organizationId;
        }

        internal IKeenAdministration GetClient()
        {
            return ProxyFactory.CreateInstance<IKeenAdministration>(KeenClient.baseUrl);
        }
    }
}
