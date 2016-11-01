using System;
using System.Collections.Generic;
using Miru.Util;

namespace Miru.Factory
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
            ["s-11"] = "\ue982",
            ["n-11"] = "\ue982",
            ["s-12"] = "\ue9b5",
            ["n-12"] = "\ue9be",
            ["s-13"] = "\ue996",
            ["n-13"] = "\ue996",
            ["s-14"] = "\ue97a",
            ["n-14"] = "\ue97a",
            ["NS"] = "X"
        };

        #endregion icon table

        private static bool isNight(int hour) => hour > 6 && hour < 18 ? false : true;

        /// <summary>
        /// 매개변수로 받은 날씨 상태에 맞는 날씨 아이콘을 반환합니다.
        /// </summary>
        /// <param name="sky">날씨 상태입니다.</param>
        /// <returns>날씨 아이콘</returns>
        public static string GetWeatherIcon(WeatherUtil.SkyCode sky, int hour)
        {
            string icon = "";

            switch (sky)
            {
                case WeatherUtil.SkyCode.Sunny:
                    icon = !isNight(hour) ? iconTable["s-01"] : iconTable["n-01"];
                    break;

                case WeatherUtil.SkyCode.PartlyCloudy:
                    icon = !isNight(hour) ? iconTable["s-02"] : iconTable["n-02"];
                    break;

                case WeatherUtil.SkyCode.MostlyCloudy:
                    icon = !isNight(hour) ? iconTable["s-03"] : iconTable["n-03"];
                    break;

                case WeatherUtil.SkyCode.MostlyCloudyAndRain:
                    icon = !isNight(hour) ? iconTable["s-04"] : iconTable["n-04"];
                    break;

                case WeatherUtil.SkyCode.MostlyCloudyAndSnow:
                    icon = !isNight(hour) ? iconTable["s-05"] : iconTable["n-05"];
                    break;

                case WeatherUtil.SkyCode.MostlyCloudyAndRainAndSnow:
                    icon = !isNight(hour) ? iconTable["s-06"] : iconTable["n-06"];
                    break;

                case WeatherUtil.SkyCode.Fog:
                    icon = !isNight(hour) ? iconTable["s-07"] : iconTable["n-07"];
                    break;

                case WeatherUtil.SkyCode.FogAndRain:
                    icon = !isNight(hour) ? iconTable["s-08"] : iconTable["n-08"];
                    break;

                case WeatherUtil.SkyCode.FogAndSnow:
                    icon = !isNight(hour) ? iconTable["s-09"] : iconTable["n-09"];
                    break;

                case WeatherUtil.SkyCode.FogAndRainAndSnow:
                    icon = !isNight(hour) ? iconTable["s-10"] : iconTable["n-10"];
                    break;

                case WeatherUtil.SkyCode.FogAndThunderstroke:
                    icon = !isNight(hour) ? iconTable["s-11"] : iconTable["n-11"];
                    break;

                case WeatherUtil.SkyCode.ThunderstormAndRain:
                    icon = !isNight(hour) ? iconTable["s-12"] : iconTable["n-12"];
                    break;

                case WeatherUtil.SkyCode.ThunderstormAndSnow:
                    icon = !isNight(hour) ? iconTable["s-13"] : iconTable["n-13"];
                    break;

                case WeatherUtil.SkyCode.ThunderstormAndRainAndSnow:
                    icon = !isNight(hour) ? iconTable["s-14"] : iconTable["n-14"];
                    break;

                case WeatherUtil.SkyCode.NoReported:
                    icon = iconTable["NS"];
                    break;
            }

            return icon;
        }
    }
}