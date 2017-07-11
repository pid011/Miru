using Miru.Factory.Weather;
using Miru.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.System.Threading;
using Windows.UI.Core;

#pragma warning disable CS4014

namespace Miru.ViewModel
{
    public class WeatherViewModel : BindableBase
    {
        private WeatherFactory weatherFactory;

        private ObservableCollection<WeatherItem> weatherItems = new ObservableCollection<WeatherItem>();
        public ObservableCollection<WeatherItem> WeatherItems => weatherItems;

        private List<WeatherItem> itemSources;

        public WeatherViewModel()
        {
            weatherFactory = new WeatherFactory(UserValue.Nx, UserValue.Ny, UserValue.ApiKey);
            GetWeatherItems();
            SetWeatherItems();
        }

        public void GetWeatherItems()
        {
            var items = weatherFactory.GetCurrentWeatherItem();
            itemSources = items;
        }

        public void SetWeatherItems()
        {
            weatherItems.Clear();
            foreach (var item in itemSources)
            {
                weatherItems.Add(item);
            }
        }
    }
}