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