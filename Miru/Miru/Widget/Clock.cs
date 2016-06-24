using System;

namespace Miru.Widget
{
	/// <summary>
	/// 각종 시간 기능을 제공합니다.
	/// </summary>
	public class Clock
	{
		/// <summary>
		/// 현재시간이 오전인지 오후인지를 문자열로 반환합니다.
		/// </summary>
		/// <returns>AM or PM</returns>
		public string GetCurrentState() => (DateTime.Now.Hour > 11) ? "PM" : "AM";

		/// <summary>
		/// 현재 시간을 문자열로 반환합니다.
		/// </summary>
		/// <returns>HH:MM</returns>
		public string GetCurrentTime()
		{
			int Hour_ = (DateTime.Now.Hour > 12) ? DateTime.Now.Hour - 12 : DateTime.Now.Hour;
			string Hour = (Hour_ < 10) ? $"0{Hour_}" : Hour_.ToString();

			string Min =
				(DateTime.Now.Minute < 10) ? $"0{DateTime.Now.Minute}" : DateTime.Now.Minute.ToString();

			return $"{Hour}:{DateTime.Now.Minute}";
		}

		/// <summary>
		/// 현재 날짜를 문자열로 반환합니다.
		/// </summary>
		/// <returns>YYYY년 MM월 DD일 W요일</returns>
		public string getCurrentWeek()
		{
			string Week = GetDayOfWeek(DateTime.Now);
			return $"{DateTime.Now.Year}년 {DateTime.Now.Month}월 {DateTime.Now.Day}일 {Week}요일";
		}

		private string GetDayOfWeek(DateTime date)
		{
			DayOfWeek day = date.DayOfWeek;
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