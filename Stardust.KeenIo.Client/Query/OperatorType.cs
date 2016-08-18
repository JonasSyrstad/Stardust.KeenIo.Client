using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    [JsonConverter(typeof(ToStringSerializer))]
    public class OperatorType
    {
        public string Operator { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Operator;
        }

        public static OperatorType EqualTo => new OperatorType { Operator = "eq" };
        public static OperatorType NotEqualTo => new OperatorType { Operator = "ne" };
        public static OperatorType LessThan => new OperatorType { Operator = "lt" };
        public static OperatorType LessThanOrEqualTo => new OperatorType { Operator = "lte" };
        public static OperatorType GreaterThan => new OperatorType { Operator = "gt" };
        public static OperatorType GreaterThanOrEqualTo => new OperatorType { Operator = "gte" };
        public static OperatorType Exists => new OperatorType { Operator = "exists" };
        public static OperatorType In => new OperatorType { Operator = "in" };
        public static OperatorType Contains => new OperatorType { Operator = "contains" };
        public static OperatorType NotContains => new OperatorType { Operator = "not_contains" };
        public static OperatorType Within => new OperatorType { Operator = "within" };
    }
}