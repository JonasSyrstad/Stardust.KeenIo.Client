#region license header
//
// ieventcollector.cs
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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.Messaging;
using Stardust.Interstellar.Rest.Annotations.UserAgent;
using Stardust.Interstellar.Rest.Service;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    [KeenWriteAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    [ErrorHandler(typeof(ErrorHandling.KeenErrorHandler))]
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
        Task AddEvents([In(InclutionTypes.Path)] string projectId, [ExtensionLevel(3)] [In(InclutionTypes.Body)] IDictionary<string, IEnumerable<object>> eventEntry);
    }

    [KeenWriteAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0/projects")]
    [ErrorHandler(typeof(ErrorHandling.KeenErrorHandler))]
    public interface IBatchEventCollector
    {
        [HttpPost]
        [Route("{projectId}/events")]
        Task<dynamic> AddEvents([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Body)] IDictionary<string, IEnumerable<object>> eventEntry);

        [HttpPost]
        [Route("{projectId}/events/{collectionName}")]
        Task<dynamic> AddEvent([In(InclutionTypes.Path)] string projectId, [In(InclutionTypes.Path)] string collectionName, [In(InclutionTypes.Body)] object eventEntry);

    }
}