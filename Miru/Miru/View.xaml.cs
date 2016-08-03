using System;
using Miru.ViewModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
using System.Threading.Tasks;

namespace Miru
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 View 페이지입니다.
    /// </summary>
    public sealed partial class View : Page
    {
        /// <summary>
        /// View 인스턴스를 초기화합니다.
        /// </summary>
        public View()
        {
            InitializeComponent();

            
        }
    }
}