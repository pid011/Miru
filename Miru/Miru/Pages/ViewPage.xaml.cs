using Miru.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.
#pragma warning disable CS4014


namespace Miru.Pages
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class ViewPage : Page
    {
        public ViewPageModel ViewPageModel { get; }

        private DispatcherTimer timeChangeTimer;
        private ThreadPoolTimer weatherLoadTimer;

        public ViewPage()
        {
            InitializeComponent();

            ViewPageModel = new ViewPageModel();

            StartTimeChangeTimer();
            StartWeatherLoadTimer();

            Unloaded += ViewPage_Unloaded;
        }

        private void ViewPage_Unloaded(object sender, RoutedEventArgs e)
        {
            timeChangeTimer.Stop();
            weatherLoadTimer.Cancel();
        }

        private void StartTimeChangeTimer()
        {
            timeChangeTimer = new DispatcherTimer();
            timeChangeTimer.Tick += (sender, e) =>
            {
                ViewPageModel.TimeViewModel.SetTime();
            };

            timeChangeTimer.Interval = TimeSpan.FromSeconds(1);
            timeChangeTimer.Start();
        }

        private void StartWeatherLoadTimer()
        {
            TimeSpan delay = TimeSpan.FromHours(3);
            bool completed = false;

            weatherLoadTimer = ThreadPoolTimer.CreateTimer((s) =>
            {
                ViewPageModel.WeatherViewModel.GetWeatherItems();

                Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    ViewPageModel.WeatherViewModel.SetWeatherItems();
                });

                completed = true;
            }, delay, (s) =>
            {
                // TODO: 작업 취소 / 완료 처리하기
                Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    if (completed)
                    {
                        // 타이머가 완료되었을 때
                    }
                    else
                    {
                        // 타이머가 취소되었을 때
                    }
                });
            });
        }
    }
}
