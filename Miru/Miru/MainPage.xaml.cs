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

// 빈 페이지 항목 템플릿에 대한 설명은 http://go.microsoft.com/fwlink/?LinkId=234238 에 나와 있습니다.

namespace Miru
{
	/// <summary>
	/// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		DispatcherTimer m_dispatcherTimer;

		public MainPage()
		{
			InitializeComponent();
			Loaded += MainPage_Loaded;
			Unloaded += MainPage_Unloaded;
		}
		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			m_dispatcherTimer = new DispatcherTimer();
			m_dispatcherTimer.Tick += M_dispatcherTimer_Tick;
			m_dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
			m_dispatcherTimer.Start();
		}

		private void M_dispatcherTimer_Tick(object sender, object e)
		{
			DateTime now = DateTime.Now;

			

			int Hour_ = (now.Hour > 12) ? now.Hour - 12 : now.Hour;
			string Hour = (Hour_ < 10) ? $"0{Hour_}" : Hour_.ToString();

			string Min = (now.Minute < 10) ? $"0{now.Minute}" : now.Minute.ToString();
			string state = (now.Hour > 11) ? "PM" : "AM";

			string Week = GetDayOfWeek(now);

			Clock_State.Text = state;
			Clock_Time.Text = $"{Hour}:{now.Minute}";
			Clock_Date.Text = $"{now.Year}년 {now.Month}월 {now.Day}일 {Week}요일";

		}

		private void MainPage_Unloaded(object sender, RoutedEventArgs e)
		{
			if(m_dispatcherTimer != null)
				if(m_dispatcherTimer.IsEnabled)
					m_dispatcherTimer.Stop();
		}

		string GetDayOfWeek(DateTime date)
		{
			var day = date.DayOfWeek;
			string Week = string.Empty;

			switch(day)
			{
				case DayOfWeek.Monday:
					Week = "월";
					break;
				case DayOfWeek.Tuesday:
					Week = "화";
					break;
				case DayOfWeek.Wednesday:
					Week = "수";
					break;
				case DayOfWeek.Thursday:
					Week = "목";
					break;
				case DayOfWeek.Friday:
					Week = "금";
					break;
				case DayOfWeek.Saturday:
					Week = "토";
					break;
				case DayOfWeek.Sunday:
					Week = "일";
					break;
				default:
					break;
			}
			return Week;
		}
	}
}
