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

        public List<WeatherItem> WeathersList => new List<WeatherItem>(weathers);

        private const string configFilename = "weather_config.json";
        private static StorageFolder folder = ApplicationData.Current.LocalFolder;


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
            public string Temperatures { get; set; }
            public string Humiditys { get; set; }
            public string SkyIcons { get; set; }
            public string FromHour { get; set; }
        }
    }
}
