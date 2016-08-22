using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class Preferences
    {
        [JsonProperty("s3_bucket_name")]
        public string S3BucketName { get; set; }
    }
}