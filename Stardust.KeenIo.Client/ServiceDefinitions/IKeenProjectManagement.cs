#region license header
//
// ikeenprojectmanagement.cs
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
using System.Threading.Tasks;
using System.Web.Http;
using Stardust.Interstellar.Rest.Annotations;
using Stardust.Interstellar.Rest.Annotations.UserAgent;
using Stardust.Interstellar.Rest.Service;

namespace Stardust.KeenIo.Client.ServiceDefinitions
{
    [KeenMasterAuthorization]
    [FixedClientUserAgent("stardust/1.0")]
    [IRoutePrefix("3.0")]
    [ErrorHandler(typeof(ErrorHandling.KeenErrorHandler))]
    public interface IKeenProjectManagement
    {
        [HttpGet]
        [Route("projects")]
        ProjectInfo[] GetProjects();

        [HttpGet]
        [Route("projects")]
        Task<ProjectInfo[]> GetProjectsAsync();

        [HttpGet]
        [Route("projects/{projectId}")]
        ProjectInfo GetProject([In(InclutionTypes.Path)] string projectId);

        [HttpGet]
        [Route("projects/{projectId}")]
        Task<ProjectInfo> GetProjectAsync([In(InclutionTypes.Path)] string projectId);

        [HttpPost]
        [Route("projects/{projectId}/keys")]
        ApiKeyDescription CreateApiKey([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpPost]
        [Route("projects/{projectId}/keys")]
        Task<ApiKeyDescription> CreateApiKeyAsync([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpGet]
        [Route("projects/{projectId}/keys")]
        ApiKeyDescription[] GetCustomKeys([In(InclutionTypes.Path)]string projectId);

        [HttpGet]
        [Route("projects/{projectId}/keys")]
        Task<ApiKeyDescription[]> GetCustomKeysAsync([In(InclutionTypes.Path)]string projectId);

        [HttpGet]
        [Route("projects/{projectId}/keys/{customKey}")]
        ApiKeyDescription GetCustomKey([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpGet]
        [Route("projects/{projectId}/keys/{customKey}")]
        Task<ApiKeyDescription> GetCustomKeyAsync([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}")]
        ApiKeyDescriptionRequest UpdateApiKey([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}")]
        Task<ApiKeyDescriptionRequest> UpdateApiKeyAsync([In(InclutionTypes.Path)]string projectId, ApiKeyDescriptionRequest apiKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/revoke")]
        void RevokeCustomKey([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/revoke")]
        Task RevokeCustomKeyAsync([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/unrevoke")]
        void UnRevokeCustomKey([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);

        [HttpPost]
        [Route("projects/{projectId}/keys/{customKey}/unrevoke")]
        Task UnRevokeCustomKeyAsync([In(InclutionTypes.Path)]string projectId, [In(InclutionTypes.Path)]string customKey);
    }
}