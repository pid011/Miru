using System;
using System.Collections.Generic;
using Miru.Factory;

namespace Miru.Util
{
    /// <summary>
    /// 날씨정보에 대한 도움이 되는 메서드를 제공합니다.
    /// </summary>
    public class WeatherUtil
    {
        public static List<char> ConvertIcon(List<SkyCode> skyState)
        {
            List<char> icons = new List<char>();
            foreach (var item in skyState)
            {
                icons.Add(WeatherIcon.GetWeatherIcon(item));
            }
            return icons;
        }

        /// <summary>
        /// 문자열을 <see cref="SkyCode"/>형태로 변환합니다.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static SkyCode ConvertSky(string position)
        {
            SkyCode result = SkyCode.NoReported;
            switch (position)
            {
                case "SKY_A01":
                case "SKY_S01":
                    result = SkyCode.Sunny;
                    break;

                case "SKY_A02":
                case "SKY_S02":
                    result = SkyCode.PartlyCloudy;
                    break;

                case "SKY_A03":
                case "SKY_S03":
                    result = SkyCode.MostlyCloudy;
                    break;

                case "SKY_A04":
                case "SKY_S04":
                    result = SkyCode.MostlyCloudyAndRain;
                    break;

                case "SKY_A05":
                case "SKY_S05":
                    result = SkyCode.MostlyCloudyAndSnow;
                    break;

                case "SKY_A06":
                case "SKY_S06":
                    result = SkyCode.MostlyCloudyAndRainAndSnow;
                    break;

                case "SKY_A07":
                case "SKY_S07":
                    result = SkyCode.Fog;
                    break;

                case "SKY_A08":
                case "SKY_S08":
                    result = SkyCode.FogAndRain;
                    break;

                case "SKY_A09":
                case "SKY_S09":
                    result = SkyCode.FogAndSnow;
                    break;

                case "SKY_A10":
                case "SKY_S10":
                    result = SkyCode.FogAndRainAndSnow;
                    break;

                case "SKY_A11":
                case "SKY_S11":
                    result = SkyCode.FogAndThunderstroke;
                    break;

                case "SKY_A12":
                case "SKY_S12":
                    result = SkyCode.ThunderstormAndRain;
                    break;

                case "SKY_A13":
                case "SKY_S13":
                    result = SkyCode.ThunderstormAndSnow;
                    break;

                case "SKY_A14":
                case "SKY_S14":
                    result = SkyCode.ThunderstormAndRainAndSnow;
                    break;
            }
            return result;
        }

        public static List<double> RoundDoubleList(List<double> list)
        {
            List<double> results = new List<double>();
            foreach (var item in list)
            {
                results.Add(Math.Round(item));
            }

            return results;
        }

        public static int ConvertInt32(double target) => Convert.ToInt32(Math.Round(target));

        /// <summary>
        /// 여러가지 하늘상태를 열거합니다.
        /// </summary>

        public enum SkyCode
        {
            /// <summary>
            /// 맑음
            /// </summary>
            Sunny,

            /// <summary>
            /// 구름조금
            /// </summary>
            PartlyCloudy,

            /// <summary>
            /// 구름많음
            /// </summary>
            MostlyCloudy,

            /// <summary>
            /// 구름많고 비
            /// </summary>
            MostlyCloudyAndRain,

            /// <summary>
            /// 구름많고 눈
            /// </summary>
            MostlyCloudyAndSnow,

            /// <summary>
            /// 구름많고 비 또는 눈
            /// </summary>
            MostlyCloudyAndRainAndSnow,

            /// <summary>
            /// 흐림
            /// </summary>
            Fog,

            /// <summary>
            /// 흐리고 비
            /// </summary>
            FogAndRain,

            /// <summary>
            /// 흐리고 눈
            /// </summary>
            FogAndSnow,

            /// <summary>
            /// 흐리고 비 또는 눈
            /// </summary>
            FogAndRainAndSnow,

            /// <summary>
            /// 흐리고 낙뢰
            /// </summary>
            FogAndThunderstroke,

            /// <summary>
            /// 뇌우, 비
            /// </summary>
            ThunderstormAndRain,

            /// <summary>
            /// 뇌우, 눈
            /// </summary>
            ThunderstormAndSnow,

            /// <summary>
            /// 뇌우, 비 또는 눈
            /// </summary>
            ThunderstormAndRainAndSnow,

            /// <summary>
            /// 알 수 없음
            /// </summary>
            NoReported
        }
    }
}