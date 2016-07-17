using System;
using System.Threading.Tasks;
using Miru.Widget;
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
            status.Text = "화면을 구성중입니다...";
            this.clock = new Clock();

            this.timer = new DispatcherTimer();
            this.timer.Tick += M_Clock_Tick;
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Start();

            this.weather = new WeatherWidget(1, 37.285944, 127.636764, "5424eae1-8e98-3d89-82e5-e9a1c589a7ba");
            weather.LoadedError += W_LoadedError;
            await this.weather.RequestWeatherAsync();

            Weather_Temp.Text = $"{ this.weather.Temperature?[0] }℃";
            status.Text = string.Empty;
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