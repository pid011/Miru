using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Miru.Widget
{
    /// <summary>
    /// 날씨정보생성을 도와줍니다.
    /// </summary>
    internal class WeatherWidget : Widget
    {
        private int version;
        private double lat;
        private double lon;
        private string appKey;

        private double currentTemp;
        private List<double> forecastTemp;
        private double currentHumidity;
        private List<double> forecastHumidity;
        private SkyCode currentSkystatus;
        private List<SkyCode> forecastSkystatus;

        public event EventHandler LoadedError;

        public List<double> Temperatures
        {
            get
            {
                List<double> list = new List<double>() { this.currentTemp };
                this.forecastTemp.ForEach(x => list.Add(x));
                return list;
            }
        }

        public List<double> Humiditys
        {
            get
            {
                List<double> list = new List<double>() { this.currentHumidity };
                this.forecastHumidity.ForEach(x => list.Add(x));
                return list;
            }
        }

        public List<SkyCode> SkyStatus
        {
            get
            {
                List<SkyCode> list = new List<SkyCode>() { this.currentSkystatus };
                this.forecastSkystatus.ForEach(x => list.Add(x));
                return list;
            }
        }

        public List<char> WeatherIcons
        {
            get
            {
                List<char> icons = new List<char>();
                WeatherIcon iconTools = new WeatherIcon();

                SkyStatus.ForEach(x => icons.Add(iconTools.GetWeatherIcon(x)));
                return icons;
            }
        }

        public enum SkyCode
        {
            Sunny,
            PartlyCloudy,
            MostlyCloudy,
            MostlyCloudyAndRain,
            MostlyCloudyAndSnow,
            MostlyCloudyAndRainAndSnow,
            Fog,
            FogAndRain,
            FogAndSnow,
            FogAndRainAndSnow,
            FogAndThunderstroke,
            ThunderstormAndRain,
            ThunderstormAndSnow,
            ThunderstormAndRainAndSnow,
            NULL
        }

        /// <summary>
        /// 날씨정보를 생성합니다.
        /// </summary>
        /// <param name="version">API 버전</param>
        /// <param name="lat">위도(Only Korea)</param>
        /// <param name="lon">경도(Only Korea)</param>
        /// <param name="appKey">SK plenet에서 제공받은 appKey</param>
        public WeatherWidget(int version, double lat, double lon, string appKey)
        {
            this.version = version;
            this.lat = lat;
            this.lon = lon;
            this.appKey = appKey;
        }

        private void LoadEvent(string msg) => LoadedError(this, new ErrorCallbackEventArgs { name = nameof(WeatherWidget), msg = msg });

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
            catch(ArgumentNullException)
            {
            }
        }

        private void CurrentWeatherJsonParse(string CurrentWeatherJson)
        {
            try
            {
                JObject obj1 = JObject.Parse(CurrentWeatherJson);

                currentTemp = (Convert.ToDouble((string)obj1["weather"]["minutely"][0]["temperature"]["tc"]));
                currentHumidity = (Convert.ToDouble((string)obj1["weather"]["minutely"][0]["humidity"]));
                string skycode = (string)obj1["weather"]["minutely"][0]["sky"]["code"];
                currentSkystatus = (GetSky(skycode));
            }
            catch(ArgumentNullException)
            {
            }
            catch(NullReferenceException)
            {
            }
        }

        private void ForecastWeatherJsonFarse(string ForecastJson)
        {
            forecastTemp = new List<double>();
            forecastHumidity = new List<double>();
            forecastSkystatus = new List<SkyCode>();
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