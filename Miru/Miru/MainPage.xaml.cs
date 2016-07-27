using System;
using Miru.Widget;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;

namespace Miru
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private Clock clock;
        private WeatherWidget weather;

        private const int PIR_SENSER_PIN = 23;
        private GpioPin pin;
        // private GpioPinValue pinValue;

        ResourceLoader rl = new ResourceLoader();

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            bool isSuccessed = InitGpio();
            this.status.Text = isSuccessed ? rl.GetString("pin_successed") : rl.GetString("pin_error");
        }
        private bool InitGpio( )
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                this.pin = null;
                return false;
            }

            this.pin = gpio.OpenPin(PIR_SENSER_PIN);
            this.pin.SetDriveMode(GpioPinDriveMode.Input);
            return true;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.clock = new Clock();
            this.timer = new DispatcherTimer();
            this.timer.Tick += M_Clock_Tick;
            this.timer.Interval = TimeSpan.FromSeconds(1);

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
        }

        private async void CreateWidget()
        {
            this.weather = new WeatherWidget(1, 37.285944, 127.636764, "5424eae1-8e98-3d89-82e5-e9a1c589a7ba");
            weather.LoadedError += W_LoadedError;
            await this.weather.RequestWeatherAsync();

            this.timer.Start();
            currentWeatherTemp.Text = $"{this.weather.Temperatures?[0]}℃";
            currentWeatherIcon.Text = this.weather.WeatherIcons?[0].ToString();

            

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
            clockState.Text = clock.GetCurrentState();
            clockTime.Text = clock.GetCurrentTime();
            clockDate.Text = clock.getCurrentWeek();
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