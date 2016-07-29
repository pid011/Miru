using System;
using Windows.ApplicationModel.Resources;

namespace Miru.View
{
    /// <summary>
    /// 각종 시간 기능을 제공합니다.
    /// </summary>
    public class Clock : View
    {
        ResourceLoader rl = new ResourceLoader();
        /// <summary>
        /// 현재시간이 오전인지 오후인지를 문자열로 반환합니다.
        /// </summary>
        /// <returns>AM or PM</returns>
        public string GetCurrentState() => (DateTime.Now.Hour > 11) ? rl.GetString("PM") : rl.GetString("AM");

        public enum Status
        {
            Morning,
            Afternoon,
            Evening,
            Night
        }

        /// <summary>
        /// 오전 오후 저녁 밤을 구분하여 반환합니다.
        /// </summary>
        public Status TimeStatus
        {
            get
            {
                int hour = DateTime.Now.Hour;
                if(hour > 5 && hour < 12)
                {
                    return Status.Morning;
                }
                else if(hour > 11 && hour < 17)
                {
                    return Status.Afternoon;
                }
                else if(hour > 16 && hour < 21)
                {
                    return Status.Evening;
                }
                return Status.Night;
            }
        }

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

            return $"{Hour}:{Min}";
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