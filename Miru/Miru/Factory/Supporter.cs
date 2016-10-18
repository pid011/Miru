using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miru.Util;
using static Miru.ViewModel.WeatherViewModel;

namespace Miru.Factory
{
    public class Supporter
    {
        public string GetAnswer()
        {
            string answer = string.Empty;

            int hour = DateTime.Now.Hour;

            if (hour > 5 && hour < 11)
            {
                answer = ResourcesString.GetString("helloMiru_moring");
            }
            else if (hour > 10 && hour < 19)
            {
                answer = ResourcesString.GetString("helloMiru_afternoon");
            }
            else if (hour > 18 && hour < 24)
            {
                answer = ResourcesString.GetString("helloMiru_evening");
            }
            else if (hour < 6)
            {
                answer = ResourcesString.GetString("helloMiru_night");
            }

            answer = $"「{answer}」";
            return answer;
        }
    }
}
