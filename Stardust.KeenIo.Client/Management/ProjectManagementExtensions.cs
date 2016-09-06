#region license header
//
// projectmanagementextensions.cs
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
using Stardust.Interstellar.Rest.Client;
using Stardust.KeenIo.Client.ServiceDefinitions;

namespace Stardust.KeenIo.Client.Management
{
    public static class ProjectManagementExtensions
    {
        public static ProjectManagementInfo CreateProject(this ManagementClient client, ProjectManagementInfoBase project) => client.GetClient().CreateProject(client.organizationId, project);
        public static async Task<ProjectManagementInfo> CreateProjectAsync(this ManagementClient client, ProjectManagementInfoBase project) => await client.GetClient().CreateProjectAsync(client.organizationId, project);
        
        public static ProjectManagementInfo GetProject(this ManagementClient client, string project) => client.GetClient().GetProject(client.organizationId, project);
        public static async Task<ProjectManagementInfo> GetProjectAsync(this ManagementClient client, string project) => await client.GetClient().GetProjectAsync(client.organizationId, project);

        public static ProjectInfo[] GetProjects(this ManagementClient client)
        {
            return ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProjects();
        }

        public static async Task<ProjectInfo[]> GetProjectsAsync(this ManagementClient client)
        {
            return  await ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProjectsAsync();
        }

        public static ProjectInfo GetProjectMetadata(this ManagementClient client, string projectId)
        {
            return ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProject(projectId);
        }

        public static async Task<ProjectInfo> GetProjectMetadataAsync(this ManagementClient client, string projectId)
        {
            return await ProxyFactory.CreateInstance<IKeenProjectManagement>(KeenClient.baseUrl).GetProjectAsync(projectId);
        }
    }
}