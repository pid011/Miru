using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miru.Util;

namespace Miru.ViewModel.Item
{
    /// <summary>
    /// 시간 기능에 대한 속성을 제공합니다.
    /// </summary>
    public class ClockItem : Item
    {
        /// <summary>
        /// 연도를 제공합니다.(yyyy)
        /// </summary>
        public string Year => _year.ToString();
        private int _year;


        /// <summary>
        /// 연중 몇번째 달인지를 제공합니다.(mm)
        /// </summary>
        public string Month => ConvertString(_month);
        private int _month;


        /// <summary>
        /// 날짜를 제공합니다.(dd)
        /// </summary>
        public string Day => ConvertString(_day);
        private int _day;


        /// <summary>
        /// 요일 이름을 제공합니다.(ddd)
        /// </summary>
        public string Week => ConvertDayOfWeekToString(_week);
        private DayOfWeek _week;


        /// <summary>
        /// 시간을 제공합니다.(hh)
        /// </summary>
        public string Hour => ConvertString(_hour);
        private int _hour;


        /// <summary>
        /// 분을 제공합니다.(nn)
        /// </summary>
        public string Minute => ConvertString(_minute);
        private int _minute;


        /// <summary>
        /// 오전 / 오후 중 하나를 제공합니다.(오전/오후)
        /// </summary>
        public string AMPM => ConvertAMPMToString(_ampm);
        private Clock.AMPM _ampm;


        /// <summary>
        /// 날짜/시간을 지정하고 <see cref="ClockItem"/>클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="year">연도를 지정합니다. 전체연도(yyyy)로 표기해야 합니다.</param>
        /// <param name="month">연중 몇번째 달인지를 지정합니다. 두자리 숫자(mm)으로 표기해야 합니다.</param>
        /// <param name="day">날짜를 지정합니다. 두자리 숫자(dd)로 표기해야 합니다.</param>
        /// <param name="week">주중 어떤 요일인지를 지정합니다.</param>
        /// <param name="hour">시간을 지정합니다. 두자리 숫자(hh)로 표기해야 합니다.</param>
        /// <param name="min">분을 지정합니다. 두자리 숫자(nn)으로 표기해야 합니다.</param>
        /// <param name="ampm">12시간 단위의 시간을 오전/오후로 지정합니다.</param>
        public ClockItem(int year, int month, int day, DayOfWeek week, int hour, int min, Clock.AMPM ampm)
        {
            this._year = year;
            this._month = month;
            this._day = day;
            this._week = week;
            this._hour = hour;
            this._minute = min;
            this._ampm = ampm;
        }

        private string ConvertAMPMToString(Clock.AMPM ampm)
        {
            string result = string.Empty;

            switch (ampm)
            {
                case Clock.AMPM.AM:
                    result = ResourcesString.GetString("am");
                    break;
                case Clock.AMPM.PM:
                    result = ResourcesString.GetString("pm");
                    break;
            }
            return result;
        }

        private string ConvertDayOfWeekToString(DayOfWeek week)
        {
            string result = string.Empty;

            switch (week)
            {
                case DayOfWeek.Monday:
                    result = ResourcesString.GetString("monday");
                    break;

                case DayOfWeek.Tuesday:
                    result = ResourcesString.GetString("tuesday");
                    break;

                case DayOfWeek.Wednesday:
                    result = ResourcesString.GetString("wednesday");
                    break;

                case DayOfWeek.Thursday:
                    result = ResourcesString.GetString("thursday");
                    break;

                case DayOfWeek.Friday:
                    result = ResourcesString.GetString("friday");
                    break;

                case DayOfWeek.Saturday:
                    result = ResourcesString.GetString("saturday");
                    break;

                case DayOfWeek.Sunday:
                    result = ResourcesString.GetString("sunday");
                    break;
            }
            return result;
        }

        private string ConvertString(int target) => target < 10 ? $"0{target}" : target.ToString();
    }
}
