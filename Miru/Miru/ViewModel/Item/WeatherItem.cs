using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Miru.Util.WeatherPlanet;

namespace Miru.ViewModel.Item
{
    /// <summary>
    /// 날씨에 대한 속성을 제공합니다.
    /// </summary>
    public class WeatherItem : Item
    {
        /// <summary>
        /// 온도를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 온도입니다.
        /// </summary>
        public Queue<double> Temperatures { get; set; }

        /// <summary>
        /// 습도를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 습도입니다.
        /// </summary>
        public Queue<double> Humiditys { get; set; }

        /// <summary>
        /// 하늘상태에 맞는 날씨 아이콘을 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 하늘상태 아이콘입니다.
        /// </summary>
        public Queue<char> SkyIcon => ConvertIcon(skyStates);
        /// <summary>
        /// 하늘상태를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 하늘상태입니다.
        /// </summary>
        public Queue<SkyCode> skyStates { get; set; }

        private static Queue<char> ConvertIcon(Queue<SkyCode> skyState)
        {
            Queue<char> icons = new Queue<char>();
            foreach (var item in skyState)
            {
                icons.Enqueue(WeatherIcon.GetWeatherIcon(item));
            }
            return icons;
        }
    }
}
