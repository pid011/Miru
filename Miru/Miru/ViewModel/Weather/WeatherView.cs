using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Miru.ViewModel
{
    /// <summary>
    /// 날씨정보생성을 도와줍니다.
    /// </summary>
    public class WeatherView : ViewModel
    {
        private int version;
        private double lat;
        private double lon;
        private string appKey;

        private Queue<double> temperature = new Queue<double>();
        private Queue<double> humidity = new Queue<double>();
        private Queue<SkyCode> skyStates = new Queue<SkyCode>();

        #region sky states
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
            NULL
        }
        #endregion

        /// <summary>
        /// 날씨정보를 생성합니다.
        /// </summary>
        /// <param name="version">API 버전</param>
        /// <param name="lat">위도(Only Korea)</param>
        /// <param name="lon">경도(Only Korea)</param>
        /// <param name="appKey">SK plenet에서 제공받은 appKey</param>
        public WeatherView(int version, double lat, double lon, string appKey)
        {
            this.version = version;
            this.lat = lat;
            this.lon = lon;
            this.appKey = appKey;
        }

        /// <summary>
        /// 날씨정보를 불러옵니다.
        /// </summary>
        /// <returns>날씨정보를 불러오는 비동기작업</returns>
        public async Task RequestWeatherAsync()
        {
            string currentWeatherUrl = $"http://apis.skplanetx.com/weather/current/minutely?version={this.version}&lat={this.lat}&lon={this.lon}&appKey={this.appKey}";
            string forecastWeatherUrl = $"http://apis.skplanetx.com/weather/forecast/3days?version={this.version}&lat={this.lat}&lon={this.lon}&appKey={this.appKey}";

            Uri currentWeatherUri = new Uri(currentWeatherUrl);
            Uri forecastWeatherUri = new Uri(forecastWeatherUrl);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string currentWeatherJson = await client.GetStringAsync(currentWeatherUri);
                    string forecastWeatherJson = await client.GetStringAsync(forecastWeatherUri);

                    CurrentWeatherJsonParse(currentWeatherJson);
                    ForecastWeatherJsonFarse(forecastWeatherJson);
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        private void CurrentWeatherJsonParse(string CurrentWeatherJson)
        {
            try
            {
                JObject obj1 = JObject.Parse(CurrentWeatherJson);
                temperature.Enqueue(Convert.ToDouble((string)obj1["weather"]["minutely"][0]["temperature"]["tc"]));
                humidity.Enqueue(Convert.ToDouble((string)obj1["weather"]["minutely"][0]["humidity"]));
                string skycode = (string)obj1["weather"]["minutely"][0]["sky"]["code"];
                skyStates.Enqueue(GetSky(skycode));
            }
            catch (FormatException)
            {
                throw;
            }
        }

        private void ForecastWeatherJsonFarse(string ForecastJson)
        {
            // TODO: 날씨 파서 구현
        }

        private SkyCode GetSky(string position)
        {
            SkyCode result = SkyCode.NULL;
            switch(position)
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
    }
}