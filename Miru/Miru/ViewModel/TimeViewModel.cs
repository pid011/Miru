using Miru.Factory.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Miru.ViewModel
{
    public class TimeViewModel : BindableBase
    {
        TimeFactory factory;
        private DispatcherTimer timer;

        private string date;
        private string time;
        private string ampm;

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }
        public string AMPM
        {
            get => ampm;
            set => SetProperty(ref ampm, value);
        }

        public TimeViewModel()
        {
            factory = new TimeFactory();
            SetTime();
        }

        public void SetTime()
        {
            var dateTime = factory.GetCurrentTime();

            Date = dateTime.Date;
            Time = dateTime.Time;
            AMPM = dateTime.AMPM;
        }
    }
}
