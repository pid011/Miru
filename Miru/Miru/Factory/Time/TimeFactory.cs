using System;
using Windows.ApplicationModel.Resources;
using Windows.Globalization.DateTimeFormatting;

namespace Miru.Factory.Time
{
    public class TimeFactory
    {
        private ResourceLoader loader = new ResourceLoader();

        public TimeViewItem GetCurrentTime()
        {
            var time = DateTime.Now;
            return new TimeViewItem(time, GetAMPM(time));
        }

        public enum AMPMS
        {
            AM,
            PM
        }

        private AMPMS GetAMPM(DateTime date) => (date.Hour > 11) ? AMPMS.PM : AMPMS.AM;

        private int ConvertHour(int hour) => hour > 12 ? hour - 12 : hour == 0 ? 12 : hour;

        private string ConvertString(AMPMS target)
        {
            string result = string.Empty;

            switch (target)
            {
                case AMPMS.AM:
                    result = loader.GetString("AM");
                    break;

                case AMPMS.PM:
                    result = loader.GetString("PM");
                    break;
            }
            return result;
        }

        private string ConvertString(DayOfWeek target)
        {
            string result = string.Empty;

            switch (target)
            {
                case DayOfWeek.Monday:
                    result = loader.GetString("Mon");
                    break;

                case DayOfWeek.Tuesday:
                    result = loader.GetString("Tue");
                    break;

                case DayOfWeek.Wednesday:
                    result = loader.GetString("Wed");
                    break;

                case DayOfWeek.Thursday:
                    result = loader.GetString("Thu");
                    break;

                case DayOfWeek.Friday:
                    result = loader.GetString("Fri");
                    break;

                case DayOfWeek.Saturday:
                    result = loader.GetString("Sat");
                    break;

                case DayOfWeek.Sunday:
                    result = loader.GetString("Sun");
                    break;
            }
            return result;
        }

        private string ConvertString(int target) => target < 10 ? $"0{target}" : target.ToString();

        public class TimeViewItem
        {
            public string Date { get; }

            public string Time { get; }

            public string AMPM { get; }

            public TimeViewItem(DateTime time, AMPMS ampm)
            {
                var factory = new TimeFactory();

                var sdatefmt = new DateTimeFormatter("shortdate");
                Date = sdatefmt.Format(time);
                Time = $"{factory.ConvertString(factory.ConvertHour(time.Hour))}:{factory.ConvertString(time.Minute)}";
                AMPM = factory.ConvertString(ampm);
            }
        }
    }
}