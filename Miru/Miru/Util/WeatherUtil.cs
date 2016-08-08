using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miru.ViewModel;
using static Miru.ViewModel.WeatherView;

namespace Miru.Util
{
    /// <summary>
    /// 날씨에 대한 속성을 제공합니다.
    /// </summary>
    public class WeatherUtil
    {
        /// <summary>
        /// 온도를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 온도입니다.
        /// </summary>
        public Queue<double> Temperatures { get; set; } = new Queue<double>();

        /// <summary>
        /// 습도를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 습도입니다.
        /// </summary>
        public Queue<double> Humiditys { get; set; } = new Queue<double>();

        /// <summary>
        /// 하늘상태에 맞는 날씨 아이콘을 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 하늘상태 아이콘입니다.
        /// </summary>
        public Queue<char> SkyIcon => ConvertIcon(skyStates);

        /// <summary>
        /// 하늘상태를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 하늘상태입니다.
        /// </summary>
        public Queue<SkyCode> skyStates { get; set; } = new Queue<SkyCode>();

        private Queue<char> ConvertIcon(Queue<SkyCode> skyState)
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
