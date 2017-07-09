using Miru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Miru.Factory.Weather.Sky;

namespace Miru.Factory.Weather
{
    public class WeatherConverter
    {
        /// <summary>
        /// 문자열을 <see cref="SkyCode"/>형태로 변환합니다.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SkyCode ConvertSky(int skyCode, int pTypeCode)
        {
            SkyCode resultCode = SkyCode.NoReported;

            switch (skyCode)
            {
                // 맑음
                case 1:
                    {
                        resultCode = SkyCode.Sunny;
                    }
                    break;

                // 구름 조금
                case 2:
                    {
                        resultCode = SkyCode.PartlyCloudy;
                    }
                    break;

                // 구름 많음
                case 3:
                    {
                        switch (pTypeCode)
                        {
                            // 비 / 눈 X
                            case 0:
                                resultCode = SkyCode.MostlyCloudy;
                                break;

                            // 비
                            case 1:
                                resultCode = SkyCode.MostlyCloudyAndRain;
                                break;

                            // 비/눈 (진눈깨비)
                            case 2:
                                resultCode = SkyCode.MostlyCloudyAndRainAndSnow;
                                break;

                            // 눈
                            case 3:
                                resultCode = SkyCode.MostlyCloudyAndSnow;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                // 흐림
                case 4:
                    {
                        switch (pTypeCode)
                        {
                            case 0:
                                resultCode = SkyCode.Fog;
                                break;

                            case 1:
                                resultCode = SkyCode.FogAndRain;
                                break;

                            case 2:
                                resultCode = SkyCode.FogAndRainAndSnow;
                                break;

                            case 3:
                                resultCode = SkyCode.FogAndSnow;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                default:
                    break;
            }

            return resultCode;
        }

        /// <summary>
        /// <see cref="SkyCode"/>를 <see cref="string"/>형태로 변환합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ConvertSkyCodeToString(SkyCode target)
        {
            string converted = "idk";

            switch (target)
            {
                case SkyCode.Sunny:
                    converted = "맑음";
                    break;
                case SkyCode.PartlyCloudy:
                    converted = "구름 조금";
                    break;
                case SkyCode.MostlyCloudy:
                    converted = "구름 많음";
                    break;
                case SkyCode.MostlyCloudyAndRain:
                    converted = "구름 많고 비";
                    break;
                case SkyCode.MostlyCloudyAndSnow:
                    converted = "구름 많고 눈";
                    break;
                case SkyCode.MostlyCloudyAndRainAndSnow:
                    converted = "구름 많고 비와 눈";
                    break;
                case SkyCode.Fog:
                    converted = "흐림";
                    break;
                case SkyCode.FogAndRain:
                    converted = "흐리고 비";
                    break;
                case SkyCode.FogAndSnow:
                    converted = "흐리고 눈";
                    break;
                case SkyCode.FogAndRainAndSnow:
                    converted = "흐리고 비와 눈";
                    break;
                default:
                    break;
            }

            return converted;
        }

        public static string ConvertBaseTime(DateTime time)
        {
            int hour = time.Hour;
            int min = time.Minute;
            var baseList = new List<int>() { 2, 5, 8, 11, 14, 17, 20, 23 };
            int baseResult = 0;

            if (hour >= baseList[7] && hour < baseList[0])
            {
                baseResult = baseList[7];
                if (hour == baseList[7])
                {
                    if (min < 11)
                    {
                        baseResult = baseList[6];
                    }
                }
            }
            else if (hour >= baseList[0] && hour < baseList[1])
            {
                baseResult = baseList[0];
                if (hour == baseList[0])
                {
                    if (min < 11)
                    {
                        baseResult = baseList[7];
                    }
                }
            }
            else
            {
                for (int i = 1; i < baseList.Count; i++)
                {
                    if (hour >= baseList[i] && hour < baseList[i + 1])
                    {
                        baseResult = baseList[i];
                        if (hour == baseList[i])
                        {
                            if (min < 11)
                            {
                                baseResult = baseList[i - 1];
                            }
                        }
                    }
                }
            }

            return MiruConverter.ConvertNumber(baseResult) + "00";
        }
    }
}
