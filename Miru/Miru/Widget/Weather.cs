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
        private PrecipitationType currentPrecipitation;
        private List<PrecipitationType> forecastPrecipitation;

        public event EventHandler LoadedError;

        public List<double> Temperature
        {
            get
            {
                List<double> list = new List<double>() { currentTemp };
                forecastTemp.ForEach(x => list.Add(x));
                return list;
            }
        }

        public List<double> Humidity
        {
            get
            {
                List<double> list = new List<double>() { currentHumidity };
                forecastHumidity.ForEach(x => list.Add(x));
                return list;
            }
        }

        public List<SkyCode> SkyStatus
        {
            get
            {
                List<SkyCode> list = new List<SkyCode>() { currentSkystatus };
                forecastSkystatus.ForEach(x => list.Add(x));
                return list;
            }
        }

        public List<PrecipitationType> Precipitation
        {
            get
            {
                List<PrecipitationType> list = new List<PrecipitationType>() { currentPrecipitation };
                forecastPrecipitation.ForEach(x => list.Add(x));
                return list;
            }
        }

        public enum PrecipitationType
        {
            Nothing,
            Rain,
            RainAndSnow,
            Snow
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

        public async Task RequestWeatherAsync()
        {
            string currentWeatherUrl = $"http://apis.skplanetx.com/weather/current/minutely?version={version}&lat={lat}&lon={lon}&appKey={appKey}";
            string forecastWeatherUrl = $"http://apis.skplanetx.com/weather/forecast/3days?version={version}&lat={lat}&lon={lon}&appKey={appKey}";

            Uri currentWeatherUri = new Uri(currentWeatherUrl);
            Uri forecastWeatherUri = new Uri(forecastWeatherUrl);
            try
            {
                string json1 = await new HttpClient().GetStringAsync(currentWeatherUri);
                string json2 = await new HttpClient().GetStringAsync(forecastWeatherUri);
                CurrentWeatherJsonParse(json1);
                ForecastWeatherJsonFarse(json2);
            }
            catch(ArgumentNullException e)
            {
                LoadEvent(e.Message);
            }
        }

        private void LoadEvent(string msg) => LoadedError(this, new ErrorCallbackEventArgs { name = nameof(WeatherWidget), msg = msg });

        private void CurrentWeatherJsonParse(string CurrentWeatherJson)
        {
            try
            {
                JObject obj1 = JObject.Parse(CurrentWeatherJson);

                currentTemp = (Convert.ToDouble((string)obj1["weather"]["minutely"][0]["temperature"]["tc"]));
                currentHumidity = (Convert.ToDouble((string)obj1["weather"]["minutely"][0]["humidity"]));
                string skycode = (string)obj1["weather"]["minutely"][0]["sky"]["code"];
                currentSkystatus = (GetSky(skycode));

                string prec = (string)obj1["weather"]["minutely"][0]["precipitation"]["type"];
                currentPrecipitation = GetPrec(Convert.ToInt32(prec));
            }
            catch(ArgumentNullException e)
            {
                LoadEvent(e.Message);
            }
            catch(NullReferenceException e)
            {
                LoadEvent(e.Message);
            }
        }

        private void ForecastWeatherJsonFarse(string ForecastJson)
        {
            forecastTemp = new List<double>();
            forecastHumidity = new List<double>();
            forecastSkystatus = new List<SkyCode>();
            forecastPrecipitation = new List<PrecipitationType>();
        }

        private PrecipitationType GetPrec(int pos)
        {
            PrecipitationType type = PrecipitationType.Nothing;
            switch(pos)
            {
                case 1:
                    type = PrecipitationType.Rain;
                    break;

                case 2:
                    type = PrecipitationType.RainAndSnow;
                    break;

                case 3:
                    type = PrecipitationType.Snow;
                    break;
            }
            return type;
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