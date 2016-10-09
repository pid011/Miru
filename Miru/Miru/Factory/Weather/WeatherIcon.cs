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

        private static Dictionary<string, char> iconTable = new Dictionary<string, char>
        {
            ["s-01"] = '1',
            ["n-01"] = '6',
            ["s-02"] = 'A',
            ["n-02"] = 'a',
            ["s-03"] = '3',
            ["n-03"] = '3',
            ["s-04"] = 'J',
            ["n-04"] = 'k',
            ["s-05"] = 'H',
            ["n-05"] = 'i',
            ["s-06"] = 'V',
            ["n-06"] = 'w',
            ["s-07"] = 'Z',
            ["n-07"] = '!',
            ["s-08"] = 'K',
            ["n-08"] = 'K',
            ["s-09"] = 'I',
            ["n-09"] = 'I',
            ["s-10"] = 'W',
            ["n-10"] = 'W',
            ["s-11"] = 'X',
            ["n-11"] = 'y',
            ["s-12"] = 'P',
            ["n-12"] = 'q',
            ["s-13"] = 'H',
            ["n-13"] = 'i',
            ["s-14"] = 'R',
            ["n-14"] = 'r',
        };

        #endregion icon table

        private static bool isNight(int hour) => hour > 6 && hour < 18 ? false : true;

        /// <summary>
        /// 매개변수로 받은 날씨 상태에 맞는 날씨 아이콘을 반환합니다.
        /// </summary>
        /// <param name="sky">날씨 상태입니다.</param>
        /// <returns>날씨 아이콘</returns>
        public static char GetWeatherIcon(WeatherUtil.SkyCode sky, int hour)
        {
            char icon = ' ';

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
            }

            return icon;
        }
    }
}