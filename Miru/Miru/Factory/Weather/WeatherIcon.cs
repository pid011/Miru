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
            ["1_d"] = "\ue60c",
            ["1_n"] = "\ue619",
            ["2_d"] = "\ue63a",
            ["2_n"] = "\ue63b",
            ["3_d"] = "\ue636",
            ["3_n"] = "\ue636",
            ["4_d"] = "\ue626",
            ["4_n"] = "\ue627",
            ["5_d"] = "\ue613",
            ["5_n"] = "\ue613",
            ["6_d"] = "\ue60d",
            ["6_n"] = "\ue60d",
            ["7_d"] = "\ue62c",
            ["7_n"] = "\ue62d",
            ["NS"] = "\uee62e"
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
                    icon = !IsNight(hour) ? iconTable["1_d"] : iconTable["1_n"];
                    break;

                case Sky.SkyCode.PartlyCloudy:
                    icon = !IsNight(hour) ? iconTable["2_d"] : iconTable["2_n"];
                    break;

                case Sky.SkyCode.MostlyCloudy:
                    icon = !IsNight(hour) ? iconTable["3_d"] : iconTable["3_n"];
                    break;

                case Sky.SkyCode.Fog:
                    icon = !IsNight(hour) ? iconTable["4_d"] : iconTable["4_n"];
                    break;

                case Sky.SkyCode.Rain:
                    icon = !IsNight(hour) ? iconTable["5_d"] : iconTable["5_n"];
                    break;

                case Sky.SkyCode.Snow:
                    icon = !IsNight(hour) ? iconTable["6_d"] : iconTable["6_n"];
                    break;

                case Sky.SkyCode.Drizzle:
                    icon = !IsNight(hour) ? iconTable["7_d"] : iconTable["7_n"];
                    break;

                case Sky.SkyCode.NoReported:
                    icon = iconTable["NS"];
                    break;
            }

            return icon;
        }
    }
}