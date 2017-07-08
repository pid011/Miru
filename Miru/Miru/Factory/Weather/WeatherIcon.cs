using System;
using System.Collections.Generic;

namespace Miru.Factory.Weather
{
    /// <summary>
    /// 날씨 아이콘에 대한 메서드를 제공합니다.
    /// </summary>
    public class WeatherIcon
    {
        #region icon table

        private static Dictionary<string, string> iconTable = new Dictionary<string, string>
        {
            ["s-01"] = "\ue999",
            ["n-01"] = "\ue95c",
            ["s-02"] = "\ue972",
            ["n-02"] = "\ue976",
            ["s-03"] = "\ue970",
            ["n-03"] = "\ue970",
            ["s-04"] = "\ue97c",
            ["n-04"] = "\ue980",
            ["s-05"] = "\ue984",
            ["n-05"] = "\ue991",
            ["s-06"] = "\ue97a",
            ["n-06"] = "\ue97a",
            ["s-07"] = "\ue92b",
            ["n-07"] = "\ue92d",
            ["s-08"] = "\ue978",
            ["n-08"] = "\ue978",
            ["s-09"] = "\ue996",
            ["n-09"] = "\ue996",
            ["s-10"] = "\ue97a",
            ["n-10"] = "\ue97a",
            ["NS"] = "X"
        };

        #endregion icon table

        private static bool IsNight(int hour)
        {
            return hour > 6 && hour < 18 ? false : true;
        }

        /// <summary>
        /// 매개변수로 받은 날씨 상태에 맞는 날씨 아이콘을 반환합니다.
        /// </summary>
        /// <param name="sky">날씨 상태입니다.</param>
        /// <returns>날씨 아이콘</returns>
        public static string GetWeatherIcon(Sky.SkyCode sky, int hour)
        {
            string icon = "";

            switch (sky)
            {
                case Sky.SkyCode.Sunny:
                    icon = !IsNight(hour) ? iconTable["s-01"] : iconTable["n-01"];
                    break;

                case Sky.SkyCode.PartlyCloudy:
                    icon = !IsNight(hour) ? iconTable["s-02"] : iconTable["n-02"];
                    break;

                case Sky.SkyCode.MostlyCloudy:
                    icon = !IsNight(hour) ? iconTable["s-03"] : iconTable["n-03"];
                    break;

                case Sky.SkyCode.MostlyCloudyAndRain:
                    icon = !IsNight(hour) ? iconTable["s-04"] : iconTable["n-04"];
                    break;

                case Sky.SkyCode.MostlyCloudyAndSnow:
                    icon = !IsNight(hour) ? iconTable["s-05"] : iconTable["n-05"];
                    break;

                case Sky.SkyCode.MostlyCloudyAndRainAndSnow:
                    icon = !IsNight(hour) ? iconTable["s-06"] : iconTable["n-06"];
                    break;

                case Sky.SkyCode.Fog:
                    icon = !IsNight(hour) ? iconTable["s-07"] : iconTable["n-07"];
                    break;

                case Sky.SkyCode.FogAndRain:
                    icon = !IsNight(hour) ? iconTable["s-08"] : iconTable["n-08"];
                    break;

                case Sky.SkyCode.FogAndSnow:
                    icon = !IsNight(hour) ? iconTable["s-09"] : iconTable["n-09"];
                    break;

                case Sky.SkyCode.FogAndRainAndSnow:
                    icon = !IsNight(hour) ? iconTable["s-10"] : iconTable["n-10"];
                    break;

                case Sky.SkyCode.NoReported:
                    icon = iconTable["NS"];
                    break;
            }

            return icon;
        }

        public static List<string> ConvertIcon(List<Sky.SkyCode> skyState)
        {
            List<string> icons = new List<string>();
            int hour = DateTime.Now.Hour;
            foreach (var item in skyState)
            {
                icons.Add(WeatherIcon.GetWeatherIcon(item, hour));

                hour += 3;
                if (hour > 24)
                {
                    hour -= 24;
                }
            }
            return icons;
        }
    }
}