using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Annotations.UserAgent;
using Stardust.Interstellar.Rest.Extensions;

namespace Stardust.KeenIo.Client
{


    [KeenWriteAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    public interface IEventCollector : IServiceWithGlobalParameters
    {
        [HttpPost]
        [Route("{projectId}/events/{collectionName}")]
        Task AddEvent([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName, [In(InclutionTypes.Body)] object eventEntry);

        //[HttpPost]
        //[Route("{projectId}/events/{collectionName}")]
        //Task AddEvents([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName, [ExtensionLevel(1)][In(InclutionTypes.Body)] IEnumerable<object> eventEntries);

        [HttpPost]
        [Route("{projectId}/events")]
        Task AddEvents([In(InclutionTypes.Path)]string projectId, [ExtensionLevel(3)][In(InclutionTypes.Body)]IDictionary<string, IEnumerable<object>> eventEntry);
    }

    public class KeenObject
    {
    }

    public static class KeenGlobalConfig
    {
        public static void SetWriterKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }

        public static void SetReaderKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }
    }
    public class KeenWriteAuthorizationAttribute : Attribute, IHeaderInspector, IHeaderHandler
    {
        internal static string WriterKey;

        public IHeaderHandler[] GetHandlers()
        {
            return new IHeaderHandler[] { this };
        }

        public void SetHeader(HttpWebRequest req)
        {
            if (req.Headers.AllKeys.Any(k => k == "Authorization")) return;
            req.Headers.Add("Authorization", GetWriterKey());
        }

        private string GetWriterKey()
        {
            if (string.IsNullOrWhiteSpace(WriterKey)) WriterKey = ConfigurationManager.AppSettings["keen:writerKey"];
            return WriterKey;
        }

        public void GetHeader(HttpWebResponse response)
        {
        }

        public void GetServiceHeader(HttpRequestHeaders headers)
        {

        }

        public void SetServiceHeaders(HttpResponseHeaders headers)
        {

        }

        public int ProcessingOrder => 0;
    }

    public class KeenReaderAuthorizationAttribute : Attribute, IHeaderInspector, IHeaderHandler
    {
        internal static string ReaderKey;

        public IHeaderHandler[] GetHandlers()
        {
            return new IHeaderHandler[] { this };
        }

        public void SetHeader(HttpWebRequest req)
        {
            req.Headers.Add("Authorization", GetWriterKey());
        }

        private string GetWriterKey()
        {
            if (string.IsNullOrWhiteSpace(ReaderKey)) ReaderKey = ConfigurationManager.AppSettings["keen:readerKey"];
            return ReaderKey;
        }

        public void GetHeader(HttpWebResponse response)
        {
        }

        public void GetServiceHeader(HttpRequestHeaders headers)
        {

        }

        public void SetServiceHeaders(HttpResponseHeaders headers)
        {

        }

        public int ProcessingOrder => 0;
    }
}
