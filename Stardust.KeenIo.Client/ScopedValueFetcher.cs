using System;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client
{
    [JsonConverter(typeof(FetcherSerializer))]
    public sealed class ScopedValueFetcher
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ScopedValueFetcher(Func<object> fetchAction)
        {
            FetchAction = fetchAction;
        }

        public ScopedValueFetcher()
        {
            
        }

        public object Fetch()
        {
            return FetchAction();
        }

        public Func<object> FetchAction { get; set; }
    }
}
