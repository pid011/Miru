using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Miru.Util;
using Miru.Factory.Clock;
using System.Threading;
using Windows.UI.Xaml;

namespace Miru.ViewModel
{
    public class ClockViewModel : BindableBase, IDisposable
    {
        private ClockFactory clockFactory;

        private DispatcherTimer clockTimer;

        private string date;
        private string time;
        private string ampms;

        public string Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.SetProperty(ref this.date, value);
            }
        }
        public string Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.SetProperty(ref this.time, value);
            }
        }
        public string AMPMS
        {
            get
            {
                return this.ampms;
            }
            set
            {
                this.SetProperty(ref this.ampms, value);
            }
        }

        public Dictionary<string, string> TimeDic
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    ["date"] = this.date,
                    ["time"] = this.time,
                    ["ampms"] = this.ampms
                };
            }
        }

        public ClockViewModel()
        {
            clockFactory = new ClockFactory();

            SetClockItem();

            clockTimer = new DispatcherTimer();
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, object e)
        {
            SetClockItem();
        }

        private void SetClockItem()
        {
            this.Date = $"{clockFactory.CurrentTime.Year}-{clockFactory.CurrentTime.Month}-{clockFactory.CurrentTime.Day} {clockFactory.CurrentTime.Week}";
            this.Time = $"{clockFactory.CurrentTime.Hour}:{clockFactory.CurrentTime.Minute}";
            this.AMPMS = clockFactory.CurrentTime.AMPM;
        }

        public void Dispose()
        {
            if (clockTimer != null)
            {
                if (clockTimer.IsEnabled == true)
                {
                    clockTimer.Stop();
                }
                clockTimer.Tick -= ClockTimer_Tick;
                
            }
        }
    }
}
