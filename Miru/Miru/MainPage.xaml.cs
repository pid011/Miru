using System;
using Miru.Widget;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Miru
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private Clock clock;
        private WeatherWidget weather;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var rl = new ResourceLoader();

            status.Text = rl.GetString("loadScreen");
            this.clock = new Clock();
            this.timer = new DispatcherTimer();
            this.timer.Tick += M_Clock_Tick;
            this.timer.Interval = TimeSpan.FromSeconds(1);

            this.weather = new WeatherWidget(1, 37.285944, 127.636764, "5424eae1-8e98-3d89-82e5-e9a1c589a7ba");
            weather.LoadedError += W_LoadedError;
            await this.weather.RequestWeatherAsync();

            // var uri = new Uri("ms-appx:///WeatherImage/0-sunny.png");

            this.timer.Start();
            Weather_Temp.Text = $"{ this.weather.Temperatures?[0] }℃";
            WeatherIcon.Text = this.weather.WeatherIcons?[0].ToString();
            status.Text = string.Empty;

            switch(this.clock.TimeStatus)
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
        }

        private void W_LoadedError(object sender, EventArgs e)
        {
            try
            {
                ErrorCallbackEventArgs args = e as ErrorCallbackEventArgs;
                status.Text = $"[{args.name}] {args.msg}";
            }
            catch(Exception)
            {
            }
        }

        private void M_Clock_Tick(object sender, object e)
        {
            Clock_State.Text = clock.GetCurrentState();
            Clock_Time.Text = clock.GetCurrentTime();
            Clock_Date.Text = clock.getCurrentWeek();
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.weather.LoadedError -= W_LoadedError;
            if(timer != null)
            {
                if(timer.IsEnabled)
                {
                    timer.Tick -= M_Clock_Tick;
                }
            }
            timer.Stop();
        }
    }
}