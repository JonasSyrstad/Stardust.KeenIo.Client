using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Stardust.Interstellar.Rest.Client;
using Stardust.Interstellar.Rest.Service;

namespace Stardust.KeenIo.Client.ErrorHandling
{
    public class KeenErrorHandler : IErrorHandler
    {
        public HttpResponseMessage ConvertToErrorResponse(Exception exception, HttpRequestMessage request) => null;

        public Exception ProduceClientException(string statusMessage, HttpStatusCode status, Exception error, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return new RestWrapperException(statusMessage, status, error);
            try
            {
                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(value);
                return new KeenException(errorMessage.Message,errorMessage.Code, new RestWrapperException(statusMessage, status, error));
            }
            catch (Exception)
            {
                return new RestWrapperException(statusMessage, status, error);
            }
        }

        public bool OverrideDefaults => false;
    }
}