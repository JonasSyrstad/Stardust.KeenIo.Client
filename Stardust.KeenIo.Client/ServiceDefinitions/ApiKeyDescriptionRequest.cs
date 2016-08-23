using Newtonsoft.Json;
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class ApiKeyDescriptionRequest
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("is_active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsActive { get; set; }

        [JsonProperty("permitted", NullValueHandling = NullValueHandling.Ignore)]
        public ApiKeyPermission[] Permitted { get; set; }

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public ApiKeyOptions Options { get; set; }
    }

    [JsonConverter(typeof(ToStringSerializer))]
    public class ApiKeyPermission
    {
        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Permission;
        }

        public string Permission { get; set; }

        public static ApiKeyPermission Write => new ApiKeyPermission { Permission = "writes" };

        public static ApiKeyPermission Query => new ApiKeyPermission { Permission = "queries" };

        public static ApiKeyPermission Schema => new ApiKeyPermission { Permission = "schema" };

        public static ApiKeyPermission SavedQueries => new ApiKeyPermission { Permission = "saved_queries" };

        public static ApiKeyPermission CachedQueries => new ApiKeyPermission { Permission = "cached_queries" };
    }
}