namespace Miru.Factory.Weather
{
    public class Sky
    {
        /// <summary>
        /// 문자열을 <see cref="SkyCode"/>형태로 변환합니다.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SkyCode GetSkyCode(int skyCode, int pTypeCode)
        {
            SkyCode resultCode = SkyCode.NoReported;

            switch (pTypeCode)
            {
                // 비 / 눈 X
                case 0:
                    {
                        switch (skyCode)
                        {
                            // 맑음
                            case 1:
                                resultCode = SkyCode.Sunny;
                                break;

                            // 구름 조금
                            case 2:
                                resultCode = SkyCode.PartlyCloudy;
                                break;

                            // 구름 많음
                            case 3:
                                resultCode = SkyCode.MostlyCloudy;
                                break;

                            // 흐림
                            case 4:
                                resultCode = SkyCode.Fog;
                                break;
                        }
                    }
                    break;

                // 비
                case 1:
                    resultCode = SkyCode.Rain;
                    break;

                // 비/눈 (진눈깨비)
                case 2:
                    resultCode = SkyCode.Drizzle;
                    break;

                // 눈
                case 3:
                    resultCode = SkyCode.Snow;
                    break;
            }

            return resultCode;
        }

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
            /// 흐림
            /// </summary>
            Fog,

            /// <summary>
            /// 비
            /// </summary>
            Rain,

            /// <summary>
            /// 눈
            /// </summary>
            Snow,

            /// <summary>
            /// 이슬비, 진눈깨비 등등...
            /// </summary>
            Drizzle,

            /// <summary>
            /// 알 수 없음
            /// </summary>
            NoReported
        }
    }
}