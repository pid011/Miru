using Miru.Utils;
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
                Time = $"{MiruConverter.ConvertNumber(MiruConverter.ConvertHour(time.Hour))}:{MiruConverter.ConvertNumber(time.Minute)}";
                AMPM = TimeConverter.ConvertString(ampm);
            }
        }
    }
}