﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miru.Factory.Clock;
using Miru.Factory;

namespace Miru.ViewModel
{
    public class MainViewModel : IDisposable
    {
        public ClockViewModel ClockViewModel { get; }
        public WeatherViewModel WeatherViewModel { get; }

        public MainViewModel()
        {
            ClockViewModel = new ClockViewModel();
            WeatherViewModel = new WeatherViewModel();
        }

        public void Dispose()
        {
            ClockViewModel.Dispose();
            WeatherViewModel.Dispose();
        }
    }
}