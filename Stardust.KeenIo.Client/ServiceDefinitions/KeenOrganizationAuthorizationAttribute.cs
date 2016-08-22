using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Stardust.Interstellar.Rest.Extensions;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    public class KeenOrganizationAuthorizationAttribute : Attribute, IHeaderInspector, IHeaderHandler
    {
        internal static string OrganizationKey;

        public IHeaderHandler[] GetHandlers()
        {
            return new IHeaderHandler[] { this };
        }

        public void SetHeader(HttpWebRequest req)
        {
            if (req.Headers.AllKeys.Any(k => k == "Authorization")) return;
            req.Headers.Add("Authorization", GetReaderKey());
        }

        private string GetReaderKey()
        {
            if (string.IsNullOrWhiteSpace(OrganizationKey)) OrganizationKey = ConfigurationManager.AppSettings["keen:organizationKey"];
            return OrganizationKey;
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