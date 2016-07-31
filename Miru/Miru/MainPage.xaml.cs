using System;
using Miru.View;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using Miru.GpioPin;
using System.Threading.Tasks;

namespace Miru
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private Clock clock;
        private WeatherWidget weather;
        private UltrasonicSenser pirSenser;
        private int count;
        private bool isSetView;

        ResourceLoader rl = new ResourceLoader();

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            this.Opacity = 0;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.pirSenser = new UltrasonicSenser();

            bool isSuccessed = pirSenser.InitGpio();

            if (isSuccessed)
            {
                this.status.Text = rl.GetString("pin_successed");
            }
            else
            {
                this.status.Text = rl.GetString("pin_error");
            }
            this.timer = new DispatcherTimer();
            this.timer.Tick += M_Clock_Tick;
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Start();

            SetView();
        }

        private void SetView()
        {
            isSetView = true;

            this.clock = new Clock();
            CreateWidget();
            switch (this.clock.TimeStatus)
            {
                case Clock.Status.Morning:
                    Center.Text = rl.GetString("helloMiru_mornig");
                    break;

                case Clock.Status.Afternoon:
                    Center.Text = rl.GetString("helloMiru_afternoon");
                    break;

                case Clock.Status.Evening:
                    Center.Text = rl.GetString("helloMiru_evening");
                    break;

                case Clock.Status.Night:
                    Center.Text = rl.GetString("helloMiru_night");
                    break;

                default:
                    Center.Text = rl.GetString("helloMiru_default");
                    break;
            }
            this.Opacity = 1;
        }

        private async void CreateWidget()
        {
            this.weather = new WeatherWidget(1, 37.285944, 127.636764, "5424eae1-8e98-3d89-82e5-e9a1c589a7ba");
            await this.weather.RequestWeatherAsync();

            currentWeatherTemp.Text = $"{this.weather.Temperatures?[0]}℃";
            currentWeatherIcon.Text = this.weather.WeatherIcons?[0].ToString();
        }

        private void M_Clock_Tick(object sender, object e)
        {
            /*
            count++;

            if (! isSetView)
            {
                if (pirSenser.pinValue == GpioPinValue.High)
                {
                    count = 0;
                    SetView();
                }
            }
            else
            {
                if (count > 9)
                {
                    count = 0;
                    CloseView();
                }
                else
                {
                    clockState.Text = clock.GetCurrentState();
                    clockTime.Text = clock.GetCurrentTime();
                    clockDate.Text = clock.getCurrentWeek();
                }
            }
            */
        }
        private void CloseView()
        {
            /*
            this.clock = null;
            this.weather = null;
            */
            this.MainGrid.Opacity = 0;
            this.isSetView = false;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (timer != null)
            {
                if (timer.IsEnabled)
                {
                    timer.Tick -= M_Clock_Tick;
                }
            }
            timer.Stop();

            this.pirSenser.Dispose();
        }
    }
}