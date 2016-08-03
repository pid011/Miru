using System;
using Miru.ViewModel.Item;
using Windows.ApplicationModel.Resources;

namespace Miru.ViewModel
{
    /// <summary>
    /// 각종 시간 기능을 제공합니다.
    /// </summary>
    public class Clock : ViewModel
    {
        /// <summary>
        /// 현재 시간을 가져옵니다.
        /// </summary>
        public ClockItem Item
        {
            get
            {
                var now = DateTime.Now;
                return new ClockItem(
                    now.Year,
                    now.Month,
                    now.Day,
                    now.DayOfWeek,
                    ConvertHour(now.Hour),
                    now.Minute,
                    GetCurrentAMPM());
            }
        }

        private AMPM GetCurrentAMPM() => (DateTime.Now.Hour > 11) ? AMPM.PM : AMPM.AM;

        private int ConvertHour(int hour) => hour > 12 ? hour - 12 : hour;

        /// <summary>
        /// 오전/오후 를 열거합니다.
        /// </summary>
        public enum AMPM
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