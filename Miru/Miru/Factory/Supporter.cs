using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Miru.ViewModel.WeatherViewModel;

namespace Miru.Factory
{
    public class Supporter
    {
        private Dictionary<string, string> timeDic;
        private List<WeatherItem> weathersList;

        public Supporter(Dictionary<string, string> timeDic, List<WeatherItem> weathersList)
        {
            this.timeDic = timeDic;
            this.weathersList = weathersList;
        }

        public string GetAnswer()
        {
            // TODO: 시간, 날씨 분석해서 대답 리턴하기
            string answer = "안녕하세요!  :)";



            return answer;
        }
    }
}
