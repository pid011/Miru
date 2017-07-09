using Miru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Miru.Factory.Time.TimeFactory;

namespace Miru.Factory.Time
{
    public class TimeConverter
    {
        public static string ConvertString(AMPMS target)
        {
            string result = string.Empty;

            switch (target)
            {
                case AMPMS.AM:
                    result = StringManager.GetString("AM");
                    break;

                case AMPMS.PM:
                    result = StringManager.GetString("PM");
                    break;
            }
            return result;
        }

        public static string ConvertString(DayOfWeek target)
        {
            string result = string.Empty;

            switch (target)
            {
                case DayOfWeek.Monday:
                    result = StringManager.GetString("Mon");
                    break;

                case DayOfWeek.Tuesday:
                    result = StringManager.GetString("Tue");
                    break;

                case DayOfWeek.Wednesday:
                    result = StringManager.GetString("Wed");
                    break;

                case DayOfWeek.Thursday:
                    result = StringManager.GetString("Thu");
                    break;

                case DayOfWeek.Friday:
                    result = StringManager.GetString("Fri");
                    break;

                case DayOfWeek.Saturday:
                    result = StringManager.GetString("Sat");
                    break;

                case DayOfWeek.Sunday:
                    result = StringManager.GetString("Sun");
                    break;
            }
            return result;
        }

        public static int ConvertHour(int hour)
        {
            return hour > 12 ? hour - 12 : hour == 0 ? 12 : hour;
        }
    }
}
