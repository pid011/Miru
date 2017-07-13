using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.ViewModel
{
    public class ViewPageModel
    {
        public TimeViewModel TimeViewModel { get; }
        public WeatherViewModel WeatherViewModel { get; }
        public NewsViewModel NewsViewModel { get; }

        public ViewPageModel()
        {
            TimeViewModel = new TimeViewModel();
            WeatherViewModel = new WeatherViewModel();
            NewsViewModel = new NewsViewModel();
        }
    }
}
