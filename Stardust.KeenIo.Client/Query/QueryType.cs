using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    [JsonConverter(typeof(ToStringSerializer))]
    public class QueryType
    {
        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Query;
        }

        public string Query { get; set; }

        public static QueryType Count => new QueryType { Query = "count" };

        public static QueryType CountUnique => new QueryType { Query = "count_unique" };

        public static QueryType Minimum => new QueryType { Query = "minimum" };

        public static QueryType Maximum => new QueryType { Query = "maximum" };

        public static QueryType Sum => new QueryType { Query = "sum" };

        public static QueryType Average => new QueryType { Query = "average" };

        public static QueryType Median => new QueryType { Query = "median" };

        public static QueryType Percentile => new QueryType { Query = "percentile" };

        public static QueryType SelectUnique => new QueryType { Query = "select_unique" };

        public static QueryType Extraction => new QueryType { Query = "extraction" };
    }
}