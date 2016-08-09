using System;

using static Miru.ViewModel.Clock.ClockView;

namespace Miru.Util
{
    /// <summary>
    /// 시간 기능에 대한 도움이 되는 메서드를 제공합니다.
    /// </summary>
    public class ClockUtil
    {
        /// <summary>
        /// <see cref="AMPMS"/>에 대응하는 문자열로 반환합니다.
        /// </summary>
        /// <param name="target">변환할 대상</param>
        /// <returns>변환된 문자열</returns>
        public static string ConvertString(AMPMS target)
        {
            string result = string.Empty;

            switch (target)
            {
                case AMPMS.AM:
                    result = ResourcesString.GetString("am");
                    break;

                case AMPMS.PM:
                    result = ResourcesString.GetString("pm");
                    break;
            }
            return result;
        }

        /// <summary>
        /// <see cref="DayOfWeek"/>에 대응하는 문자열로 반환합니다.
        /// </summary>
        /// <param name="target">변환할 대상</param>
        /// <returns>변환된 문자열</returns>
        public static string ConvertString(DayOfWeek target)
        {
            string result = string.Empty;

            switch (target)
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

        /// <summary>
        /// 변환할 정수가 10 미만인 경우 0을 앞에 붙혀서 반환합니다.
        /// </summary>
        /// <param name="target">변환할 대상</param>
        /// <returns>변환된 문자열</returns>
        public static string ConvertString(int target) => target < 10 ? $"0{target}" : target.ToString();
    }
}