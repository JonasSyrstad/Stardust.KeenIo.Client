using System;
using System.Collections.Generic;
using Stardust.Interstellar.Rest.Common;
using Stardust.Interstellar.Rest.Extensions;
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
}