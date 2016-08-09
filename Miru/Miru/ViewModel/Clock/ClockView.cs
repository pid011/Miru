using System;
using Miru.Util;

namespace Miru.ViewModel.Clock
{
    /// <summary>
    /// 각종 시간 기능을 제공합니다.
    /// </summary>
    public class ClockView : IClock
    {
        /// <summary>
        /// 연도를 제공합니다.(yyyy)
        /// </summary>
        public string Year => ClockUtil.ConvertString(DateTime.Now.Year);

        /// <summary>
        /// 연중 몇번째 달인지를 제공합니다.(mm)
        /// </summary>
        public string Month => ClockUtil.ConvertString(DateTime.Now.Month);

        /// <summary>
        /// 날짜를 제공합니다.(dd)
        /// </summary>
        public string Day => ClockUtil.ConvertString(DateTime.Now.Day);

        /// <summary>
        /// 요일 이름을 제공합니다.(ddd)
        /// </summary>
        public string Week => ClockUtil.ConvertString(DateTime.Now.DayOfWeek);

        /// <summary>
        /// 시간을 제공합니다.(hh)
        /// </summary>
        public string Hour => ClockUtil.ConvertString(ConvertHour(DateTime.Now.Hour));

        /// <summary>
        /// 분을 제공합니다.(nn)
        /// </summary>
        public string Minute => ClockUtil.ConvertString(DateTime.Now.Minute);

        /// <summary>
        /// 오전 / 오후 중 하나를 제공합니다.(오전/오후)
        /// </summary>
        public string AMPM => ClockUtil.ConvertString(GetCurrentAMPM());

        private AMPMS GetCurrentAMPM() => (DateTime.Now.Hour > 11) ? AMPMS.PM : AMPMS.AM;

        /// <summary>
        /// 24시간 단위 시간을 12시간 단위 시간으로 변환합니다.
        /// </summary>
        /// <param name="hour">변환할 시간입니다.</param>
        /// <returns>변환된 시간</returns>
        private int ConvertHour(int hour) => hour > 12 ? hour - 12 : hour;

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