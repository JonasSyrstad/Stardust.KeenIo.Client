#region license header
//
// timeframe.cs
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
    public class TimeFrame
    {
        public string Timeframe { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Timeframe;
        }

        public static TimeFrame ThisMinute => new TimeFrame { Timeframe = "this_minute" };
        public static TimeFrame ThisHour => new TimeFrame { Timeframe = "this_hour" };
        public static TimeFrame ThisDay => new TimeFrame { Timeframe = "this_day" };
        public static TimeFrame ThisWeek => new TimeFrame { Timeframe = "this_week" };
        public static TimeFrame ThisMonth => new TimeFrame { Timeframe = "this_month" };
        public static TimeFrame ThisYear => new TimeFrame { Timeframe = "this_year" };
        public static TimeFrame ThisNMinutes(int n) => new TimeFrame { Timeframe = $"this_{n}_minutes" };
        public static TimeFrame ThisNHours(int n) => new TimeFrame { Timeframe = $"this_{n}_hours" };
        public static TimeFrame ThisNDays(int n) => new TimeFrame { Timeframe = $"this_{n}_days" };
        public static TimeFrame ThisNWeeks(int n) => new TimeFrame { Timeframe = $"this_{n}_weeks" };
        public static TimeFrame ThisNMonths(int n) => new TimeFrame { Timeframe = $"this_{n}_months" };
        public static TimeFrame ThisNYears(int n) => new TimeFrame { Timeframe = $"this_{n}_years" };
        public static TimeFrame PreviousNMinutes(int n) => new TimeFrame { Timeframe = $"previous_{n}_minutes" };
        public static TimeFrame PreviousNHours(int n) => new TimeFrame { Timeframe = $"previous_{n}_hours" };
        public static TimeFrame PreviousNDays(int n) => new TimeFrame { Timeframe = $"previous_{n}_days	" };
        public static TimeFrame PreviousNWeeks(int n) => new TimeFrame { Timeframe = $"previous_{n}_weeks" };
        public static TimeFrame PreviousNMonths(int n) => new TimeFrame { Timeframe = $"previous_{n}_months" };
        public static TimeFrame PreviousNYears(int n) => new TimeFrame { Timeframe = $"previous_{n}_years" };
        public static TimeFrame PreviousMinute => new TimeFrame { Timeframe = "previous_minute" };
        public static TimeFrame PreviousHour => new TimeFrame { Timeframe = "previous_hour" };
        public static TimeFrame Yesterday => new TimeFrame { Timeframe = "yesterday" };
        public static TimeFrame PreviousDay => new TimeFrame { Timeframe = "previous_day" };
        public static TimeFrame PreviousWeek => new TimeFrame { Timeframe = "previous_week" };
        public static TimeFrame PreviousMonth => new TimeFrame { Timeframe = "previous_month" };
        public static TimeFrame PreviousYear => new TimeFrame { Timeframe = "previous_year" };
    }
}