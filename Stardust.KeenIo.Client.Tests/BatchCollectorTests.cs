using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Common;
using Xunit;
using Xunit.Abstractions;

namespace Stardust.KeenIo.Client.Tests
{
    public class BatchCollectorTests : IDisposable
    {
        private readonly ITestOutputHelper output;

        private static readonly string ProjectId = ConfigurationManager.AppSettings["keen:projectId"];

        private Stopwatch timer;

        public BatchCollectorTests(ITestOutputHelper output)
        {
            timer = Stopwatch.StartNew();
            KeenBatchClient.VerboseLogging = false;  
            this.output = output;
            var logger = new Logger(output);
            ExtensionsFactory.SetServiceLocator(logger);
            //System.Net.GlobalProxySelection.Select = WebProxy.GetDefaultProxy();
            new KeenConfiguration { ProjectId = ProjectId, BatchSize = 10, SwalowException = false }.Initialize();
            KeenBatchClient.StartEventPump();
        }

        [Fact]
        public async Task  AddEvent()
        {
            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun" });
            await KeenBatchClient.ShutdownEventPumpAsync();
        }


        [Fact]
        public async Task AddEvents()
        {
            var rnd=new Random(2);
            for (var i = 0; i < 1000; i++)
            {
                KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = $"TestRun_{i}" });
                await Task.Delay(rnd.Next(0, 10));
            }

            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun_1000" });

            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun_1002" });

            KeenBatchClient.AddEvent("batchTests", new { TimeStamp = DateTime.UtcNow, Name = "TestRun_1004" });
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        void IDisposable.Dispose()
        {
            try
            {
                output.WriteLine("Shutting down");
                KeenBatchClient.ShutdownEventPump();
            }
            catch (Exception ex)
            {
                output.WriteLine(ex.Message);
            }
            output.WriteLine($"ellapsed time {timer.ElapsedMilliseconds}ms");
        }
    }
}