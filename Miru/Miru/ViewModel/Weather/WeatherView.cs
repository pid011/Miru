using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Miru.Util;
using Miru.ViewModel.Item;
using Newtonsoft.Json.Linq;
using static Miru.Util.WeatherPlanet;

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
        public async Task<WeatherItem> GetWeatherItem()
        {
            WeatherPlanet weatherPlanet = new WeatherPlanet(this.version, this.lat, this.lon, this.appKey);
            string currentWeatherJson = await weatherPlanet.GetCurrentWeatherJsonAsync();
            string forecastWeatherJson = await weatherPlanet.GetForecastWeatherJsonAsync();

            return weatherPlanet.WeatherJsonParse(currentWeatherJson, forecastWeatherJson);
        }
    }
}