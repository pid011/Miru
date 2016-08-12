using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Miru.Util;
using Miru.ViewModel.Weather;
using Newtonsoft.Json.Linq;

namespace Miru.ViewModel
{
    /// <summary>
    /// 날씨정보생성을 도와줍니다.
    /// </summary>
    public class WeatherView : IWeather
    {
        private int version;
        private double lat;
        private double lon;
        private string appKey;

        public Queue<double> Temperatures => RoundDouble(temperatures);
        private Queue<double> temperatures;

        public Queue<double> Humiditys => RoundDouble(humiditys);
        private Queue<double> humiditys;

        public Queue<char> SkyIcons => WeatherUtil.ConvertIcon(SkyStates);

        public Queue<WeatherUtil.SkyCode> SkyStates => skyStates;
        private Queue<WeatherUtil.SkyCode> skyStates;

        private Queue<double> RoundDouble(Queue<double> queue)
        {
            Queue<double> results = new Queue<double>();
            foreach (var item in queue)
            {
                results.Enqueue(Math.Round(item));
            }

            return results;
        }

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
        public async Task CreateWeatherItem()
        {
            var jsons = await GetWeatherJsonAsync();
            WeatherJsonParse(jsons[0], jsons[1]);
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

        private void WeatherJsonParse(string currentWeatherJson, string forecastWeatherJson)
        {
            temperatures = new Queue<double>();
            humiditys = new Queue<double>();
            skyStates = new Queue<WeatherUtil.SkyCode>();

            try
            {
                JObject cwObj = JObject.Parse(currentWeatherJson);
                cwObj = (JObject) cwObj["weather"]["minutely"][0];
                temperatures.Enqueue(Convert.ToDouble((string) cwObj["temperature"]["tc"]));
                humiditys.Enqueue(Convert.ToDouble((string) cwObj["humidity"]));
                string skycode = (string) cwObj["sky"]["code"];
                skyStates.Enqueue(WeatherUtil.ConvertSky(skycode));

                JObject fwObj = JObject.Parse(forecastWeatherJson);
                fwObj = (JObject) fwObj["weather"]["forecast3days"][0]["fcst3hour"];
                for (int i = 4; i < 25; i += 3)
                {
                    temperatures.Enqueue(Convert.ToDouble((string) fwObj["temperature"][$"temp{i}hour"]));
                    humiditys.Enqueue(Convert.ToDouble((string) fwObj["humidity"][$"rh{i}hour"]));
                    skyStates.Enqueue(WeatherUtil.ConvertSky((string) fwObj["sky"][$"code{i}hour"]));
                }
            }
            catch (FormatException)
            {
                throw;
            }
        }
    }
}