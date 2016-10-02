using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Miru.Util;
using Miru.ViewModel;
using Newtonsoft.Json.Linq;

namespace Miru.Factory
{
    public class WeatherFactory
    {
        /*
        public class WeatherItem
        {
            /// <summary>
            /// 온도를 3시간 단위로 제공합니다.
            /// 첫번째 요소는 현재 온도입니다.
            /// </summary>
            public List<string> Temperatures { get; }

            /// <summary>
            /// 습도를 3시간 단위로 제공합니다.
            /// 첫번째 요소는 현재 습도입니다.
            /// </summary>
            public List<string> Humiditys { get; }

            /// <summary>
            /// 하늘상태에 맞는 날씨 아이콘을 3시간 단위로 제공합니다.
            /// 첫번째 요소는 현재 하늘상태 아이콘입니다.
            /// </summary>
            public List<string> SkyIcons { get; }
            public int Count => Temperatures.Count;

            public WeatherItem(List<double> temp, List<double> hum, List<WeatherUtil.SkyCode> skyStates)
            {
                this.Temperatures = new List<string>();
                temp.ForEach(x => Temperatures.Add(CommonUtil.ConvertString(WeatherUtil.ConvertInt32(x))));

                this.Humiditys = new List<string>();
                hum.ForEach(x => Humiditys.Add(CommonUtil.ConvertString(WeatherUtil.ConvertInt32(x))));

                this.SkyIcons = new List<string>();
                WeatherUtil.ConvertIcon(skyStates).ForEach(x => SkyIcons.Add(x.ToString()));
            }
        }
        */
        private int version;
        private double lat;
        private double lon;
        private string appKey;

        public List<WeatherViewModel.WeatherItem> CurrentWeather
        {
            get
            {
                var list = new List<WeatherViewModel.WeatherItem>();
                for (int i = 0; i < temperatures.Count; i++)
                {
                    list.Add(new WeatherViewModel.WeatherItem()
                    {
                        Temperatures = 
                            $"{CommonUtil.ConvertString(WeatherUtil.ConvertInt32(temperatures[i]))}{ResourcesString.GetString("temp")}",
                        Humiditys = $"{CommonUtil.ConvertString(WeatherUtil.ConvertInt32(humiditys[i]))}%",
                        SkyIcons = WeatherUtil.ConvertIcon(skyStates)[i].ToString()
                    });
                }
                int hour = DateTime.Now.Hour;
                int day = 0;
                foreach (var item in list)
                {
                    hour += 3;
                    if (hour > 24)
                    {
                        hour -= 24;
                        day++;
                    }
                    item.FromHour = $"{hour}{ResourcesString.GetString("hour")}";
                    #region 날짜표현
                    switch (day)
                    {
                        case 0:
                            item.FromHour = $"{ResourcesString.GetString("today")} {item.FromHour}";
                            break;
                        case 1:
                            item.FromHour = $"{ResourcesString.GetString("tomorrow")} {item.FromHour}";
                            break;
                        case 2:
                            item.FromHour = $"{ResourcesString.GetString("the_day_after_tomorrow")} {item.FromHour}";
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                return list;
            }
        }

        private List<double> temperatures;
        private List<double> humiditys;
        private List<WeatherUtil.SkyCode> skyStates;

        /// <summary>
        /// 날씨정보를 가져오기 위한 몇가지 정보를 지정하고
        /// <see cref="WeatherFactory"/>클래스의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="version">API 버전</param>
        /// <param name="lat">위도(Only Korea)</param>
        /// <param name="lon">경도(Only Korea)</param>
        /// <param name="appKey">SK plenet에서 제공받은 appKey</param>
        public WeatherFactory(int version, double lat, double lon, string appKey)
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
            temperatures = new List<double>();
            humiditys = new List<double>();
            skyStates = new List<WeatherUtil.SkyCode>();

            try
            {
                JObject cwObj = JObject.Parse(currentWeatherJson);
                cwObj = (JObject) cwObj["weather"]["minutely"][0];
                temperatures.Add(Convert.ToDouble((string) cwObj["temperature"]["tc"]));
                humiditys.Add(Convert.ToDouble((string) cwObj["humidity"]));
                string skycode = (string) cwObj["sky"]["code"];
                skyStates.Add(WeatherUtil.ConvertSky(skycode));

                JObject fwObj = JObject.Parse(forecastWeatherJson);
                fwObj = (JObject) fwObj["weather"]["forecast3days"][0]["fcst3hour"];
                for (int i = 4; i < 25; i += 3)
                {
                    temperatures.Add(Convert.ToDouble((string) fwObj["temperature"][$"temp{i}hour"]));
                    humiditys.Add(Convert.ToDouble((string) fwObj["humidity"][$"rh{i}hour"]));
                    skyStates.Add(WeatherUtil.ConvertSky((string) fwObj["sky"][$"code{i}hour"]));
                }
            }
            catch (FormatException)
            {
                throw new FormatException(ResourcesString.GetString("format_exception"));
            }
        }
    }
}