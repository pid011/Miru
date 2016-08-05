using System;
using System.Collections.Generic;
using Miru.Util;

using static Miru.Util.WeatherPlanet;

namespace Miru.ViewModel
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
            ["s-04"] = 'K',
            ["n-04"] = 'k',
            ["s-05"] = 'H',
            ["n-05"] = 'h',
            ["s-06"] = 'T',
            ["n-06"] = 't',
            ["s-07"] = '…',
            ["n-07"] = '!',
            ["s-08"] = 'M',
            ["n-08"] = 'm',
            ["s-09"] = 'I',
            ["n-09"] = 'i',
            ["s-10"] = 'U',
            ["n-10"] = 'u',
            ["s-11"] = 'Y',
            ["n-11"] = 'y',
            ["s-12"] = 'Q',
            ["n-12"] = 'q',
            ["s-13"] = 'S',
            ["n-13"] = 's',
            ["s-14"] = 'R',
            ["n-14"] = 'r',
        };
        #endregion

        private static bool isNight()
        {
            var now = DateTime.Now;
            // 낮과 밤의 평균 길이는 12시간, 12시간이므로
            return now.Hour > 6 && now.Hour < 20 ? true : false;
        }

        /// <summary>
        /// 매개변수로 받은 날씨 상태에 맞는 날씨 아이콘을 반환합니다.
        /// </summary>
        /// <param name="sky">날씨 상태입니다.</param>
        /// <returns>날씨 아이콘</returns>
        public static char GetWeatherIcon(SkyCode sky)
        {
            char icon = '1';

            switch(sky)
            {
                case SkyCode.Sunny:
                    icon = !isNight() ? iconTable["s-01"] : iconTable["n-01"];
                    break;

                case SkyCode.PartlyCloudy:
                    icon = !isNight() ? iconTable["s-02"] : iconTable["n-02"];
                    break;

                case SkyCode.MostlyCloudy:
                    icon = !isNight() ? iconTable["s-03"] : iconTable["n-03"];
                    break;

                case SkyCode.MostlyCloudyAndRain:
                    icon = !isNight() ? iconTable["s-04"] : iconTable["n-04"];
                    break;

                case SkyCode.MostlyCloudyAndSnow:
                    icon = !isNight() ? iconTable["s-05"] : iconTable["n-05"];
                    break;

                case SkyCode.MostlyCloudyAndRainAndSnow:
                    icon = !isNight() ? iconTable["s-06"] : iconTable["n-06"];
                    break;

                case SkyCode.Fog:
                    icon = !isNight() ? iconTable["s-07"] : iconTable["n-07"];
                    break;

                case SkyCode.FogAndRain:
                    icon = !isNight() ? iconTable["s-08"] : iconTable["n-08"];
                    break;

                case SkyCode.FogAndSnow:
                    icon = !isNight() ? iconTable["s-09"] : iconTable["n-09"];
                    break;

                case SkyCode.FogAndRainAndSnow:
                    icon = !isNight() ? iconTable["s-10"] : iconTable["n-10"];
                    break;

                case SkyCode.FogAndThunderstroke:
                    icon = !isNight() ? iconTable["s-11"] : iconTable["n-11"];
                    break;

                case SkyCode.ThunderstormAndRain:
                    icon = !isNight() ? iconTable["s-12"] : iconTable["n-12"];
                    break;

                case SkyCode.ThunderstormAndSnow:
                    icon = !isNight() ? iconTable["s-13"] : iconTable["n-13"];
                    break;

                case SkyCode.ThunderstormAndRainAndSnow:
                    icon = !isNight() ? iconTable["s-14"] : iconTable["n-14"];
                    break;
            }

            return icon;
        }
    }
}