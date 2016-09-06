#region license header
//
// keenconfiguration.cs
// This file is part of Stardust.KeenIo.Client
//
// Author: Jonas Syrstad (jonas.syrstad@dnvgl.com), http://no.linkedin.com/in/jonassyrstad/) 
// Copyright (c) 2016 Jonas Syrstad. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#endregion license header
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

        /// <summary>
        /// set the bacth clients batch threshold. (min value 10)
        /// </summary>
        public int? BatchSize { get; set; }
    }
}