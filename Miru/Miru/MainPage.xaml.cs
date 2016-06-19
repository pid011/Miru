using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
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
using System.Net.Http;
using Newtonsoft.Json.Linq;

using Miru.Widget;

namespace Miru
{
	/// <summary>
	/// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		DispatcherTimer m_Clock;
		Clock clock;
		WeatherInfo weather;

		public MainPage()
		{
			InitializeComponent();
			Loaded += MainPage_Loaded;
			Unloaded += MainPage_Unloaded;
		}

		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			weather = new WeatherInfo(1, 37.285944, 127.636764, "5424eae1-8e98-3d89-82e5-e9a1c589a7ba");
			weather.Create();

			clock = new Clock();
			m_Clock = new DispatcherTimer();
			m_Clock.Tick += M_Clock_Tick;
			m_Clock.Interval = TimeSpan.FromSeconds(1);
			m_Clock.Start();


			Weather_Temp.Text = $"{weather.Temperature}℃";
		}

		private void M_Clock_Tick(object sender, object e)
		{
			Clock_State.Text = clock.GetCurrentState();
			Clock_Time.Text = clock.GetCurrentTime();
			Clock_Date.Text = clock.getCurrentWeek();

		}

		private void MainPage_Unloaded(object sender, RoutedEventArgs e)
		{
			if(m_Clock != null)
				if(m_Clock.IsEnabled)
					m_Clock.Stop();
		}

	}
}
