﻿#region license header
//
// keenanalyticclient.cs
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
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client
{
    public static class KeenAnalyticClient
    {
        public static dynamic MultiAnalysis(this MultiQuery query) => KeenClient.KeenInspector.MultiQuery(KeenClient.projectId, query);

        public static async Task<dynamic> MultiAnalysisAsync(this MultiQuery query) => await KeenClient.KeenInspector.MultiQueryAsync(KeenClient.projectId, query);

        public static async Task<dynamic> FunnelAsync(this FunnelQuery query) => await KeenClient.KeenInspector.FunnelAsync(KeenClient.projectId, query);

        public static dynamic Funnel(this FunnelQuery query) => KeenClient.KeenInspector.Funnel(KeenClient.projectId, query);
    }
}