using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Miru.Factory;
using Windows.Storage;
using Miru.Util;

namespace Miru.ViewModel
{
    public class WeatherViewModel : BindableBase, IDisposable
    {
        private WeatherFactory weatherFactory;

        private ObservableCollection<WeatherItem> weathers = new ObservableCollection<WeatherItem>();
        public ObservableCollection<WeatherItem> Weathers => this.weathers;

        private int version;
        private double lat;
        private double lon;
        private string appKey;

        public WeatherViewModel()
        {
            this.IntializeWeatherFactory();
            this.SetWeatherItem();
        }

        private async void SetWeatherItem()
        {
            await weatherFactory.CreateWeatherItem();
            for (int i = 0; i < weatherFactory.CurrentWeather.Count; i++)
            {
                weathers.Add(weatherFactory.CurrentWeather[i]);
            }
        }

        private void IntializeWeatherFactory()
        {
            GetSettingItem();
            this.weatherFactory = new WeatherFactory(this.version, this.lat, this.lon, this.appKey);
        }

        private void GetSettingItem()
        {
            this.version = WeatherConfig.version;
            this.lat = WeatherConfig.lat;
            this.lon = WeatherConfig.lon;
            this.appKey = WeatherConfig.appKey;
        }

        public void Dispose()
        {
            
        }

        public class WeatherItem
        {
            /// <summary>
            /// 온도
            /// </summary>
            public string Temperatures { get; set; }
            /// <summary>
            /// 습도
            /// </summary>
            public string Humiditys { get; set; }
            /// <summary>
            /// 하늘상태 아이콘
            /// </summary>
            public string SkyIcons { get; set; }
            /// <summary>
            /// 날씨정보 제공 시간
            /// </summary>
            public string FromHour { get; set; }
            /// <summary>
            /// 하늘상태
            /// </summary>
            public WeatherUtil.SkyCode SkyCode { get; set; }
        }
    }
}
