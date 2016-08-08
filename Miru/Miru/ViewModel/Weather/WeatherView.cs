using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Miru.Util;
using Newtonsoft.Json.Linq;

namespace Miru.ViewModel
{
    /// <summary>
    /// 날씨정보생성을 도와줍니다.
    /// </summary>
    public class WeatherView : WeatherUtil
    {
        private int version;
        private double lat;
        private double lon;
        private string appKey;


        /// <summary>
        /// 날씨정보를 가져오기 위한 몇가지 정보를 지정하고 
        /// <see cref="WeatherView"/>클래스의 인스턴스를 초기화합니다.
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
        /// 날씨정보를 비동기로 가져옵니다.
        /// </summary>
        /// <returns>날씨정보</returns>
        public async Task<WeatherUtil> GetWeatherItem()
        {
            var jsons = await GetWeatherJsonAsync();
            var item = WeatherJsonParse(jsons[0], jsons[1]);

            return item;
        }

        /// <summary>
        /// 날씨정보를 비동기로 가져옵니다.
        /// 키값: [0] => 현재날씨, [1] => 일기예보
        /// </summary>
        /// <returns>Json형태의 날씨정보</returns>
        private async Task<Dictionary<int, string>> GetWeatherJsonAsync()
        {
            string cwUrl = $"http://apis.skplanetx.com/weather/current/minutely?version={this.version}&lat={this.lat}&lon={this.lon}&appKey={this.appKey}";
            Uri cwUri = new Uri(cwUrl);

            string fwUrl = $"http://apis.skplanetx.com/weather/forecast/3days?version={this.version}&lat={this.lat}&lon={this.lon}&appKey={this.appKey}";
            Uri fwUri = new Uri(fwUrl);


            string cwJson;
            string fwJson;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    cwJson = await client.GetStringAsync(cwUri);
                    fwJson = await client.GetStringAsync(fwUri);
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }

            return new Dictionary<int, string>()
            {
                [0] = cwJson,
                [1] = fwJson
            };
        }

        private WeatherUtil WeatherJsonParse(string currentWeatherJson, string forecastWeatherJson)
        {
            WeatherUtil item = new WeatherUtil();
            try
            {
                JObject cwObj = JObject.Parse(currentWeatherJson);
                cwObj = (JObject) cwObj["weather"]["minutely"][0];
                item.Temperatures.Enqueue(Convert.ToDouble((string) cwObj["temperature"]["tc"]));
                item.Humiditys.Enqueue(Convert.ToDouble((string) cwObj["humidity"]));
                string skycode = (string) cwObj["sky"]["code"];
                item.skyStates.Enqueue(GetSky(skycode));


                JObject fwObj = JObject.Parse(forecastWeatherJson);
                fwObj = (JObject) fwObj["weather"]["forecast3days"][0]["fcst3hour"];
                // fwObj = JObject.Parse(fwObj["fcst3hour"].ToString());
                // fwObj = (JObject) fwArr[0]["fcst3hour"]; // error

                for (int i = 4; i < 25; i += 3)
                {
                    item.Temperatures.Enqueue(Convert.ToDouble((string) fwObj["temperature"][$"temp{i}hour"]));
                    item.Humiditys.Enqueue(Convert.ToDouble((string) fwObj["humidity"][$"rh{i}hour"]));
                    item.skyStates.Enqueue(GetSky((string) fwObj["sky"][$"code{i}hour"]));
                }

            }
            catch (FormatException)
            {
                throw;
            }

            return item;
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