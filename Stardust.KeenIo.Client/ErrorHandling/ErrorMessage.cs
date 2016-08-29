using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ErrorHandling
{
    public class ErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error_code")]
        public string Code { get; set; }
    }
}