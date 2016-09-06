#region license header
//
// operatortype.cs
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

namespace Stardust.KeenIo.Client.Query
{
    [JsonConverter(typeof(ToStringSerializer))]
    public class OperatorType
    {
        public string Operator { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Operator;
        }

        public static OperatorType EqualTo => new OperatorType { Operator = "eq" };
        public static OperatorType NotEqualTo => new OperatorType { Operator = "ne" };
        public static OperatorType LessThan => new OperatorType { Operator = "lt" };
        public static OperatorType LessThanOrEqualTo => new OperatorType { Operator = "lte" };
        public static OperatorType GreaterThan => new OperatorType { Operator = "gt" };
        public static OperatorType GreaterThanOrEqualTo => new OperatorType { Operator = "gte" };
        public static OperatorType Exists => new OperatorType { Operator = "exists" };
        public static OperatorType In => new OperatorType { Operator = "in" };
        public static OperatorType Contains => new OperatorType { Operator = "contains" };
        public static OperatorType NotContains => new OperatorType { Operator = "not_contains" };
        public static OperatorType Within => new OperatorType { Operator = "within" };
    }
}