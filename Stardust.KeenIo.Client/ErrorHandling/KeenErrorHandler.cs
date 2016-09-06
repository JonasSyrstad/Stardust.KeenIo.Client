#region license header
//
// keenerrorhandler.cs
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
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Stardust.Interstellar.Rest.Client;
using Stardust.Interstellar.Rest.Service;

namespace Stardust.KeenIo.Client.ErrorHandling
{
    public class KeenErrorHandler : IErrorHandler
    {
        public HttpResponseMessage ConvertToErrorResponse(Exception exception, HttpRequestMessage request) => null;

        public Exception ProduceClientException(string statusMessage, HttpStatusCode status, Exception error, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return new RestWrapperException(statusMessage, status, error);
            try
            {
                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(value);
                return new KeenException(errorMessage.Message,errorMessage.Code, new RestWrapperException(statusMessage, status, error));
            }
            catch (Exception)
            {
                return new RestWrapperException(statusMessage, status, error);
            }
        }

        public bool OverrideDefaults => false;
    }
}