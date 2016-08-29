using System;
using System.Runtime.Serialization;

namespace Stardust.KeenIo.Client.ErrorHandling
{
    public class KeenException : Exception
    {
        public string ErrorCode { get; set; }

        public KeenException()
        {
        }

        public KeenException(string message) : base(message)
        {
        }

        public KeenException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public KeenException(string message,string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected KeenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
