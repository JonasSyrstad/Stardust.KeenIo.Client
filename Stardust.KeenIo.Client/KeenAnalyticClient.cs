using System.Threading.Tasks;
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client
{
    public static class KeenAnalyticClient
    {
        public static dynamic MultiAnalysis(this MultiQuery query) => KeenClient.KeenInspector.MultiQuery(KeenClient.projectId, query);

        public static async Task<object> MultiAnalysisAsync(this MultiQuery query) => await KeenClient.KeenInspector.MultiQueryAsync(KeenClient.projectId, query);

        public static async Task<object> FunnelAsync(this FunnelQuery query) => await KeenClient.KeenInspector.FunnelAsync(KeenClient.projectId, query);

        public static dynamic Funnel(this FunnelQuery query) => KeenClient.KeenInspector.Funnel(KeenClient.projectId, query);
    }
}