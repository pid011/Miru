﻿using System;
using Miru.Util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 빈 페이지 항목 템플릿에 대한 설명은 http://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace Miru
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 Backgound 페이지입니다.
    /// </summary>
    public sealed partial class Background : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Background 인스턴스를 초기화합니다.
        /// </summary>
        public Background()
        {
            this.InitializeComponent();

            this.Unloaded += Background_Unloaded;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Background_Unloaded(object sender, RoutedEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            var currentDistance = Senser.Distance.GetDistance();
            if (currentDistance < Config.Senser.distance)
            {
                timer.Stop();
                this.Frame.Navigate(typeof(View));
            }
        }
    }
}