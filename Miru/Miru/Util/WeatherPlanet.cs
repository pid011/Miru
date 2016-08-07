using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Miru.ViewModel;
using Miru.ViewModel.Item;
using Newtonsoft.Json.Linq;

namespace Miru.Util
{
    /// <summary>
    /// SK Weather Planet API를 활용하여 날씨정보 관련 메서드를 제공합니다.
    /// </summary>
    public class WeatherPlanet
    {
        private int version;
        private double lat;
        private double lon;
        private string appKey;

        private double currentTemperature;
        private double currentHumidity;
        private SkyCode currentSkyState;

        private Queue<double> forecastTemperatures = new Queue<double>();
        private Queue<double> forecastHumiditys = new Queue<double>();
        private Queue<SkyCode> forecastSkyStates = new Queue<SkyCode>();

        /// <summary>
        /// 날씨정보를 가져오기 위한 몇가지 정보를 지정하고 
        /// <see cref="WeatherPlanet"/>클래스의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="version">API 버전</param>
        /// <param name="lat">위도(Only Korea)</param>
        /// <param name="lon">경도(Only Korea)</param>
        /// <param name="appKey">SK plenet에서 제공받은 appKey</param>

        public WeatherPlanet(int version, double lat, double lon, string appKey)
        {
            this.version = version;
            this.lat = lat;
            this.lon = lon;
            this.appKey = appKey;
        }

        /// <summary>
        /// 날씨정보를 비동기로 가져옵니다.
        /// </summary>
        /// <returns>Json형태의 날씨정보</returns>
        public async Task<Dictionary<int, string>> GetWeatherJsonAsync()
        {
            string currentWeatherUrl = $"http://apis.skplanetx.com/weather/current/minutely?version={this.version}&lat={this.lat}&lon={this.lon}&appKey={this.appKey}";
            Uri currentWeatherUri = new Uri(currentWeatherUrl);

            string forecastWeatherUrl = $"http://apis.skplanetx.com/weather/forecast/3days?version={this.version}&lat={this.lat}&lon={this.lon}&appKey={this.appKey}";
            Uri forecastWeatherUri = new Uri(forecastWeatherUrl);


            string currentWeatherJson;
            string forecastWeatherJson;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    currentWeatherJson = await client.GetStringAsync(currentWeatherUri);
                    forecastWeatherJson = await client.GetStringAsync(forecastWeatherUri);
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }

            return new Dictionary<int, string>()
            {
                [1] = currentWeatherJson,
                [2] = forecastWeatherJson
            };
        }

        /// <summary>
        /// Json포맷의 문자열을 분석하여 날씨정보로 변환합니다.
        /// </summary>
        /// <param name="currentWeatherJson">현재날씨 Json</param>
        /// <param name="forecastWeatherJson">일기예보 Json</param>
        /// <returns>날씨정보</returns>
        public WeatherItem WeatherJsonParse(string currentWeatherJson, string forecastWeatherJson)
        {
            CurrentWeatherJsonParse(currentWeatherJson);
            ForecastWeatherJsonParse(forecastWeatherJson);

            Queue<double> temperatures = new Queue<double>();
            temperatures.Enqueue(currentTemperature);
            foreach (var item in forecastTemperatures)
            {
                temperatures.Enqueue(item);
            }

            Queue<double> humiditys = new Queue<double>();
            humiditys.Enqueue(currentHumidity);
            foreach (var item in forecastHumiditys)
            {
                humiditys.Enqueue(item);
            }

            Queue<SkyCode> skyStates = new Queue<SkyCode>();
            skyStates.Enqueue(currentSkyState);
            foreach (var item in forecastSkyStates)
            {
                skyStates.Enqueue(item);
            }

            return new WeatherItem() { Temperatures = temperatures, Humiditys = humiditys, skyStates = skyStates };
        }

        private void CurrentWeatherJsonParse(string currentWeatherJson)
        {
            try
            {
                JObject obj = JObject.Parse(currentWeatherJson);
                //TODO: obj = JObject.Parse((string) obj["weather"]["minutely"][0]);
                currentTemperature = Convert.ToDouble((string) obj["weather"]["minutely"][0]["temperature"]["tc"]);
                currentHumidity = Convert.ToDouble((string) obj["weather"]["minutely"][0]["humidity"]);
                string skycode = (string) obj["weather"]["minutely"][0]["sky"]["code"];
                currentSkyState = GetSky(skycode);
            }
            catch (FormatException)
            {
                throw;
            }
        }

        private void ForecastWeatherJsonParse(string forecastWeatherJson)
        {
            try
            {
                JObject obj = JObject.Parse(forecastWeatherJson);
                obj = JObject.Parse((string) obj["weather"]["forecast3days"]["fcst3hour"]);

                for (int i = 4; i < 25; i += 3)
                {
                    forecastTemperatures.Enqueue(Convert.ToDouble((string) obj["temperature"][$"temp{i}hour"]));
                    forecastHumiditys.Enqueue(Convert.ToDouble((string) obj["humidity"][$"rh{i}hour"]));
                    forecastSkyStates.Enqueue(GetSky((string) obj["sky"][$"code{i}hour"]));
                }
            }
            catch (FormatException)
            {
                throw;
            }
        }

        private SkyCode GetSky(string position)
        {
            SkyCode result = SkyCode.NULL;
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

    }
}
