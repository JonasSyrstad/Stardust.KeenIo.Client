using Newtonsoft.Json;

namespace Stardust.KeenIo.Client.Query
{
    [JsonConverter(typeof(ToStringSerializer))]
    public class Timezone
    {
        public string TimeZone { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return TimeZone;
        }

        public static Timezone USEastern => new Timezone { TimeZone = "US/Eastern" };
        public static Timezone USCentral => new Timezone { TimeZone = "US/Central" };
        public static Timezone USMountain => new Timezone
                                                 {
                                                     TimeZone = "US/Mountain"
                                                 };

        public static Timezone USPacific => new Timezone
                                                {
                                                    TimeZone = "US/Pacific"
                                                };
        public static Timezone USAlaska => new Timezone
                                               {
                                                   TimeZone = "US/Alaska"
                                               };
        public static Timezone USHawaii => new Timezone
                                               {
                                                   TimeZone = "US/Hawaii"
                                               };
        public static Timezone EuropeAmsterdam => new Timezone
                                                      {
                                                          TimeZone = "Europe/Amsterdam"
                                                      };
        public static Timezone EuropeLondon => new Timezone
                                                   {
                                                       TimeZone = "Europe/London"
                                                   };
        public static Timezone EuropeParis => new Timezone
                                                  {
                                                      TimeZone = "Europe/Paris"
                                                  };
        public static Timezone EuropePrague => new Timezone
                                                   {
                                                       TimeZone = "Europe/Prague"
                                                   };
        public static Timezone EuropeStockholm => new Timezone
                                                      {
                                                          TimeZone = "Europe/Stockholm"
                                                      };
        public static Timezone EuropeCopenhagen => new Timezone
                                                       {
                                                           TimeZone = "Europe/Copenhagen"
                                                       };

        public static Timezone AfricaCasablanca => new Timezone
                                                       {
                                                           TimeZone = "Africa/Casablanca"
                                                       };

        public static Timezone AfricaNairobi => new Timezone
                                                    {
                                                        TimeZone = "Africa/Nairobi"
                                                    };

        public static Timezone AsiaSingapore => new Timezone
                                                    {
                                                        TimeZone = "Asia/Singapore"
                                                    };
        public static Timezone AustraliaSydney => new Timezone
                                                      {
                                                          TimeZone = "Australia/Sydney"
                                                      };
        public static Timezone AsiaDubai => new Timezone
                                                {
                                                    TimeZone = "Asia/Dubai"
                                                };
        public static Timezone AsiaIstanbul => new Timezone
                                                   {
                                                       TimeZone = "Asia/Istanbul"
                                                   };
        public static Timezone AsiaJakarta => new Timezone
                                                  {
                                                      TimeZone = "Asia/Jakarta"
                                                  };
        public static Timezone AsiaTokyo => new Timezone
                                                {
                                                    TimeZone = "Asia/Tokyo"
                                                };
        public static Timezone AmericaSao_Paulo => new Timezone
                                                       {
                                                           TimeZone = "America/Sao_Paulo"
                                                       };

        public static Timezone AustraliaPerth => new Timezone
                                                     {
                                                         TimeZone = "Australia/Perth"
                                                     };
        public static Timezone EuropeIstanbul => new Timezone
                                                     {
                                                         TimeZone = "Europe/Istanbul"
                                                     };
        public static Timezone PacificAuckland => new Timezone
                                                      {
                                                          TimeZone = "Pacific/Auckland"
                                                      };
        public static Timezone UTC => new Timezone
                                          {
                                              TimeZone = "UTC"
                                          };
    }
}