#region license header
//
// keenglobalconfig.cs
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
using Newtonsoft.Json;
using Stardust.KeenIo.Client.ServiceDefinitions;
using Stardust.Interstellar.Rest.Common;

namespace Stardust.KeenIo.Client
{
    public static class KeenGlobalConfig
    {
        static KeenGlobalConfig()
        {
            new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.None,
                    
                }.AddClientSerializer<IKeenInspection>()
                .AddClientSerializer<IEventCollector>()
                .AddClientSerializer<IKeenAdministration>()
                .AddClientSerializer<IKeenProjectManagement>();
        }

        public static void SetWriterKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }

        public static void SetReaderKey(string key)
        {
            KeenWriteAuthorizationAttribute.WriterKey = key;
        }
    }
}