using System;
using Miru.Util;

namespace Miru.Factory.Clock
{
    /// <summary>
    /// 각종 시간 기능을 제공합니다.
    /// </summary>
    public class ClockFactory
    {
        public class ClockItem
        {
            /// <summary>
            /// 연도를 제공합니다.(yyyy)
            /// </summary>
            public string Year { get; }
            /// <summary>
            /// 연중 몇번째 달인지를 제공합니다.(mm)
            /// </summary>
            public string Month { get; }
            /// <summary>
            /// 날짜를 제공합니다.(dd)
            /// </summary>
            public string Day { get; }
            /// <summary>
            /// 요일 이름을 제공합니다.(ddd)
            /// </summary>
            public string Week { get; }
            /// <summary>
            /// 시간을 제공합니다.(hh)
            /// </summary>
            public string Hour { get; }
            /// <summary>
            /// 분을 제공합니다.(nn)
            /// </summary>
            public string Minute { get; }
            /// <summary>
            /// 오전 / 오후 중 하나를 제공합니다.(오전/오후)
            /// </summary>
            public string AMPM { get; }

            public ClockItem(int year, int month, int day, DayOfWeek week, int hour, int minute, AMPMS ampm)
            {
                this.Year = ClockUtil.ConvertString(year);
                this.Month = ClockUtil.ConvertString(month);
                this.Day = ClockUtil.ConvertString(day);
                this.Week = ClockUtil.ConvertString(week);
                this.Hour = ClockUtil.ConvertString(ClockUtil.ConvertHour(hour));
                this.Minute = ClockUtil.ConvertString(minute);
                this.AMPM = ClockUtil.ConvertString(ampm);
            }
        }

        /// <summary>
        /// 현재 시간을 제공합니다.
        /// </summary>
        public ClockItem CurrentTime
        {
            get
            {
                return new ClockItem(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    DateTime.Now.DayOfWeek,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    GetCurrentAMPM());
            }
        }

        private AMPMS GetCurrentAMPM() => (DateTime.Now.Hour > 11) ? AMPMS.PM : AMPMS.AM;

        /// <summary>
        /// 오전/오후 를 열거합니다.
        /// </summary>
        public enum AMPMS
        {
            /// <summary>
            /// 오전
            /// </summary>
            AM,

            /// <summary>
            /// 오후
            /// </summary>
            PM
        }
    }
}