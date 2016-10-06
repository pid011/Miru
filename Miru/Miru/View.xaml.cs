using System;
using Miru.Factory;
using Miru.Factory.Clock;
using Miru.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Miru
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 View 페이지입니다.
    /// </summary>
    public sealed partial class View : Page
    {
        public MainViewModel ViewModel { get; }

        private DispatcherTimer timer;

        /// <summary>
        /// View 인스턴스를 초기화합니다.
        /// </summary>
        public View()
        {
            InitializeComponent();

            this.Unloaded += View_Unloaded;

            this.ViewModel = new MainViewModel();

            timer = new DispatcherTimer();
            /*
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Timer_End;
            timer.Start();
            */
        }

        private void Timer_End(object sender, object e)
        {
            timer.Stop();
            this.Frame.Navigate(typeof(Background));
        }

        private void View_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Dispose();
        }
    }
}