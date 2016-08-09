using System;
using Miru.ViewModel;
using Miru.ViewModel.Clock;
using Miru.ViewModel.Weather;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Miru
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 View 페이지입니다.
    /// </summary>
    public sealed partial class View : Page
    {
        private DispatcherTimer clockTimer;

        private WeatherView weatherView;
        private IWeather weather;

        private ClockView clockView;
        private IClock clock;

        private int count;

        /// <summary>
        /// View 인스턴스를 초기화합니다.
        /// </summary>
        public View()
        {
            InitializeComponent();

            this.Opacity = 0;
            this.Loaded += View_Loaded;
            this.Unloaded += View_Unloaded;
        }

        private void View_Unloaded(object sender, RoutedEventArgs e)
        {
            if (clockTimer != null)
            {
                clockTimer.Stop();
            }
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            weatherView = new WeatherView(1, 37.285944, 127.636764, "5424eae1-8e98-3d89-82e5-e9a1c589a7ba");
            await weatherView.CreateWeatherItem();
            weather = weatherView as IWeather;

            clockView = new ClockView();
            clock = clockView as IClock;

            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimer_Tick;
            count = 0;
            clockTimer.Start();

            SetWeatherTexts();
            SetClockTexts();

            this.Opacity = 1;
        }

        private void SetWeatherTexts()
        {
            this.currentWeatherTemp.Text = $"{weather.Temperatures.Dequeue()}℃";
            this.currentWeatherIcon.Text = weather.SkyIcons.Dequeue().ToString();

            this.forecastWeatherTemp1.Text = $"{weather.Temperatures.Dequeue()}℃";
            this.forecastWeatherTemp2.Text = $"{weather.Temperatures.Dequeue()}℃";
            this.forecastWeatherTemp3.Text = $"{weather.Temperatures.Dequeue()}℃";
            this.forecastWeatherTemp4.Text = $"{weather.Temperatures.Dequeue()}℃";
            this.forecastWeatherTemp5.Text = $"{weather.Temperatures.Dequeue()}℃";

            this.forecastWeatherIcon1.Text = weather.SkyIcons.Dequeue().ToString();
            this.forecastWeatherIcon2.Text = weather.SkyIcons.Dequeue().ToString();
            this.forecastWeatherIcon3.Text = weather.SkyIcons.Dequeue().ToString();
            this.forecastWeatherIcon4.Text = weather.SkyIcons.Dequeue().ToString();
            this.forecastWeatherIcon5.Text = weather.SkyIcons.Dequeue().ToString();
        }

        private void SetClockTexts()
        {
            this.clockTime.Text = $"{clock.Hour}:{clock.Minute}";
            this.clockState.Text = clock.AMPM;
            this.clockDate.Text = $"{clock.Year}-{clock.Month}-{clock.Day} {clock.Week}";
        }

        private void ClockTimer_Tick(object sender, object e)
        {
            SetClockTexts();

            count++;
            if (count > 19)
            {
                clockTimer.Stop();
                this.Frame.Navigate(typeof(Background));
            }
        }
    }
}