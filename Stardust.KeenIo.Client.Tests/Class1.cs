using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Client;
using Xunit;

namespace Stardust.KeenIo.Client.Tests
{

    public class ClientTest
    {
        [Fact]
        public async Task AddEntryTest()
        {
            var client = ProxyFactory.CreateInstance<IEventCollector>("https://api.keen.io");
            client.SetGlobalProperty("host", Environment.MachineName).SetGlobalProperty("user",Environment.UserName);

            await client.AddEvent("560c2d6e672e6c1204fba8d5", "test", new { TimeStamp = DateTime.UtcNow, Name = "UnitTest" });

        }
    }
}
