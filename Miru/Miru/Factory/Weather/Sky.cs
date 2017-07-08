namespace Miru.Factory.Weather
{
    public class Sky
    {
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
            /// 알 수 없음
            /// </summary>
            NoReported
        }
    }
}