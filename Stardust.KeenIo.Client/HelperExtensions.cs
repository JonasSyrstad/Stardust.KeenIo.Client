#region license header
//
// helperextensions.cs
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
using Stardust.KeenIo.Client.Query;

namespace Stardust.KeenIo.Client
{
    public static class HelperExtensions
    {
        public static long GetEventCount(this IEnumerable<CollectionInfo> collections, TimeFrame timeFrame)
        {
            long totalEvents = 0;
            foreach (var collectionInfo in collections)
            {
                var result = collectionInfo.GetEventCount( timeFrame);
                totalEvents +=result ;
            }
            return totalEvents;
        }

        public static long GetEventCount(this CollectionInfo collectionInfo,TimeFrame timeFrame)
        {
            var result = QueryType.Count.Query(new QueryBody { EventCollection = collectionInfo.Name, TimeFrame = timeFrame, Timezone = Timezone.UTC });
            return (long)result.result;
        }

        public static async Task<long> GetEventCountAsync(this IEnumerable<CollectionInfo> collections, TimeFrame timeFrame)
        {
            long totalEvents = 0;
            foreach (var collectionInfo in collections)
            {
                var result = await collectionInfo.GetEventCountAsync(timeFrame);
                totalEvents += result;
            }
            return totalEvents;
        }

        public static async Task<long> GetEventCountAsync(this CollectionInfo collectionInfo,TimeFrame timeFrame )
        {
            var result = await QueryType.Count.QueryAsync(new QueryBody { EventCollection = collectionInfo.Name, TimeFrame = timeFrame, Timezone = Timezone.UTC });
            return (long)result.result; ;
        }

        
    }
}