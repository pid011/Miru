using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miru.Factory.Clock;
using Miru.Factory.Weather;
using Miru.Factory;

namespace Miru.ViewModel
{
    public class MainViewModel : IDisposable
    {
        public ClockViewModel ClockViewModel { get; }

        public MainViewModel()
        {
            ClockViewModel = new ClockViewModel();
        }

        public void Dispose()
        {
            ClockViewModel.Dispose();
        }
    }
}
