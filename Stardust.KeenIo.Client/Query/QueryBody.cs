#region license header
//
// querybody.cs
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
using System.Dynamic;
using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    

    public class QueryBody : DynamicObject
    {
        private Dictionary<string, object> dynamicMembers = new Dictionary<string, object>();
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dynamicMembers.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            try
            {
                if (dynamicMembers.ContainsKey(binder.Name)) dynamicMembers[binder.Name] = value;
                else
                    dynamicMembers.Add(binder.Name, value);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return dynamicMembers.Keys;
        }

        [JsonProperty("event_collection")]
        public string EventCollection { get; set; }

        [JsonProperty("timeframe", ItemConverterType = typeof(ToStringSerializer), NullValueHandling = NullValueHandling.Ignore)]
        public TimeFrame TimeFrame { get; set; }

        [JsonProperty("group_by", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupBy { get; set; }

        [JsonProperty("timezone", ItemConverterType = typeof(ToStringSerializer), NullValueHandling = NullValueHandling.Ignore)]
        public Timezone Timezone { get; set; }


        [JsonProperty("target_property", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetProperty { get; set; }

        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public FilterPart Filters { get; set; }


    }
}