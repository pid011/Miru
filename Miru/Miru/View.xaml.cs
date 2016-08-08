using System;
using Miru.ViewModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using System.Threading.Tasks;
using Miru.ViewModel.Clock;

namespace Miru
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 View 페이지입니다.
    /// </summary>
    public sealed partial class View : Page
    {
        DispatcherTimer clockTimer;

        private WeatherView weatherView;
        private ClockView clockView;
        private IClock clock;

        int count;

        /// <summary>
        /// View 인스턴스를 초기화합니다.
        /// </summary>
        public View()
        {
            InitializeComponent();

            // this.Opacity = 0;
            this.Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            weatherView = new WeatherView(1, 37.285944, 127.636764, "5424eae1-8e98-3d89-82e5-e9a1c589a7ba");

            clockView = new ClockView();
            clock = clockView as IClock;

            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += ClockTimer_Tick;
            count = 0;
            clockTimer.Start();

            // this.currentWeatherTemp.Text = $"{weatherItem.Temperatures.Dequeue()}℃";
            // .currentWeatherIcon.Text = weatherItem.SkyIcon.Dequeue().ToString();

            this.Opacity = 1;

        }

        private void ClockTimer_Tick(object sender, object e)
        {
            this.clockTime.Text = $"{clock.Hour}:{clock.Minute}";
            this.clockState.Text = clock.AMPM;
            this.clockDate.Text = $"{clock.Year}-{clock.Month}-{clock.Day} {clock.Week}";
            /*
            count++;
            if (count > 19)
            {
                this.Frame.Navigate(typeof(Background));
            }
            */
        }
    }
}