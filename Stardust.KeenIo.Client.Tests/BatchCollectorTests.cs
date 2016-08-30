using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Stardust.Interstellar.Rest.Common;
using Stardust.Interstellar.Rest.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Stardust.KeenIo.Client.Tests
{
    public class Logger : ILogger, IServiceLocator
    {
        private readonly ITestOutputHelper output;

        public Logger(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Error(Exception error)
        {
            output.WriteLine(error.Message);
            output.WriteLine(error.StackTrace);
            if (error.InnerException != null) Error(error.InnerException);
        }

        public void Message(string message)
        {
            output.WriteLine(message);
        }

        public void Message(string format, params object[] args)
        {
            output.WriteLine(format, args);
        }

        public T GetService<T>()
        {
            if (this is T)
                return (T)(this as object);
            return default(T);
        }

        public IEnumerable<T> GetServices<T>()
        {
            return new List<T>();
        }
    }

    public class BatchCollectorTests : IDisposable
    {
        private readonly ITestOutputHelper output;

        private static readonly string ProjectId = ConfigurationManager.AppSettings["keen:projectId"];
        public BatchCollectorTests(ITestOutputHelper output)
        {
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
        }
    }
}