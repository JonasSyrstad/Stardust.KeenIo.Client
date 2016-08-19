using System;
using System.Collections.Generic;
using System.Configuration;

namespace Stardust.KeenIo.Client
{
    /// <summary>
    /// Configures the keen event collector service
    /// </summary>
    public class KeenConfiguration
    {
        /// <summary>
        /// The base url for keen.io api. appSettings key: keen:baseUrl
        /// </summary>
        /// <remarks>Mandatory info</remarks>
        public string BaseUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(baseUrl))
                    if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["keen:baseUrl"]))
                        return ConfigurationManager.AppSettings["keen:baseUrl"];
                    else return "https://api.keen.io";
                return baseUrl;
            }
            set
            {
                baseUrl = value;
            }
        }

        private string readerKey;

        private string writerKey;

        private string projectId;

        private string baseUrl;

        /// <summary>
        /// The project id from keen.io. appSettings key: keen:projectId
        /// </summary>
        /// <remarks>Mandatory info</remarks>
        public string ProjectId
        {
            get
            {
                return string.IsNullOrWhiteSpace(projectId) ? ConfigurationManager.AppSettings["keen:projectId"] : projectId;
            }
            set
            {
                projectId = value;
            }
        }

        public KeenConfiguration()
        {

        }

        public bool SwalowException { get; set; } = true;

        public KeenConfiguration(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId)) throw new ArgumentNullException(nameof(projectId), "project id is mandatory");
            ProjectId = projectId;
        }
        /// <summary>
        /// The reader key from keen.io, required for analytics/queries. appSettings key: keen:readerKey
        /// </summary>
        public string ReaderKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(readerKey)) return ConfigurationManager.AppSettings["keen:readerKey"];
                return readerKey;
            }
            set
            {
                readerKey = value;
            }
        }

        /// <summary>
        /// The writer key from keen.io, required for event capturing. appSettings key: keen:writerKey
        /// </summary>
        public string WriterKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(writerKey)) return ConfigurationManager.AppSettings["keen:writerKey"];
                return writerKey;
            }
            set
            {
                writerKey = value;
            }
        }

        /// <summary>
        /// A collection containing properties to be added to all events sent to keen.io. 
        /// This is the propper place to put environment info and other static information.
        /// </summary>
        public Dictionary<string, object> GlobalProperties { get; set; }
    }
}