using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 http://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace Miru
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 Backgound 페이지입니다.
    /// </summary>
    public sealed partial class Background : Page
    {
        /// <summary>
        /// Background 인스턴스를 초기화합니다.
        /// </summary>
        public Background()
        {
            this.InitializeComponent();

            if (!Control.IsInitialized)
            {
                Control.InitializeGpio();
            }
            this.Loaded += Background_Loaded;
        }

        private async void Background_Loaded(object sender, RoutedEventArgs e)
        {
            Control.Distance = 90;
            await Control.WaitDistanceAsync();
            this.Frame.Navigate(typeof(View));
        }
    }
}
