using System;
using System.Threading.Tasks;
using Xunit;

namespace Stardust.KeenIo.Client.Tests
{
    public class BatchCollectorTests : IDisposable
    {

        public BatchCollectorTests()
        {
            KeenClient.Initialize(new KeenConfiguration { ProjectId = "560c2d6e672e6c1204fba8d5", BatchSize = 10 });
            KeenBatchClient.StartEventPump();
        }

        [Fact]
        public void AddEvent()
        {
            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun" });
        }


        [Fact]
        public void AddEvents()
        {
            for (var i = 0; i < 100; i++)
            {
                KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = $"TestRun_{i}" });
            }

            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun_1000" });

            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun_1002" });

            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun_1004" });
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            try
            {
                KeenBatchClient.ShutdownEventPump();
            }
            catch (Exception)
            {

            }
        }
    }
}