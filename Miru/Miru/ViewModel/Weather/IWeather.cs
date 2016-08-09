using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miru.Util;

namespace Miru.ViewModel.Weather
{
    /// <summary>
    /// 날씨 정보에 대한 속성을 제공합니다.
    /// </summary>
    interface IWeather
    {
        /// <summary>
        /// 온도를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 온도입니다.
        /// </summary>
        Queue<double> Temperatures { get; }
        /// <summary>
        /// 습도를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 습도입니다.
        /// </summary>
        Queue<double> Humiditys { get; }
        /// <summary>
        /// 하늘상태에 맞는 날씨 아이콘을 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 하늘상태 아이콘입니다.
        /// </summary>
        Queue<char> SkyIcons { get; }
        /// <summary>
        /// 하늘상태를 3시간 단위로 제공합니다.
        /// 첫번째 요소는 현재 하늘상태입니다.
        /// </summary>
        Queue<WeatherUtil.SkyCode> SkyStates { get; }
    }
}
