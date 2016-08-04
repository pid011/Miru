using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.ViewModel.Item
{
    /// <summary>
    /// 날씨에 대한 속성을 제공합니다.
    /// </summary>
    public class WeatherItem : Item
    {
        /// <summary>
        /// 온도를 3시간 단위로 제공합니다.
        /// 키값은 항상 1부터 시작하여 1, 4, 7... 순으로 3씩 늘어납니다.
        /// 1은 현재 온도입니다.
        /// </summary>
        public Dictionary<int, double> Temperatures => temperatures;
        private Dictionary<int, double> temperatures;

        /// <summary>
        /// 습도를 3시간 단위로 제공합니다.
        /// 키값은 항상 1부터 시작하여 1, 4, 7... 순으로 3씩 늘어납니다.
        /// 1은 현재 습도입니다.
        /// </summary>
        public Dictionary<int, double> Humiditys => humiditys;
        private Dictionary<int, double> humiditys;

        /// <summary>
        /// 하늘상태에 맞는 날씨 아이콘을 3시간 단위로 제공합니다.
        /// 키값은 항상 1부터 시작하여 1, 4, 7... 순으로 3씩 늘어납니다.
        /// 1은 현재 하늘상태입니다.
        /// </summary>
        public Dictionary<int, char> SkyIcon => ConvertIcon(skys);
        private Dictionary<int, WeatherView.SkyCode> skys;


        /// <summary>
        /// 온도, 습도, 하늘상태를 지정하고 <see cref="WeatherItem"/>클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="temperatures">
        /// 온도입니다.
        /// 첫번째 요소는 항상 현재 온도가 들어가야 하며, 그 다음부터는 3시간 단위의 온도가 들어가야 합니다.
        /// </param>
        /// <param name="humiditys">
        /// 습도입니다.
        /// 첫번째 요소는 항상 현재 습도가 들어가야 하며, 그 다음부터는 3시간 단위의 습도가 들어가야 합니다.
        /// </param>
        /// <param name="skyStates">
        /// 하늘상태입니다.
        /// 첫번째 요소는 항상 현재 하늘상태가 들어가야 하며, 그 다음부터는 3시간 단위의 하늘상태가 들어가야 합니다.
        /// </param>
        public WeatherItem(Queue<double> temperatures, Queue<double> humiditys, Queue<WeatherView.SkyCode> skyStates)
        {
            int i = 1;

            this.temperatures = new Dictionary<int, double>();
            foreach (var temp in temperatures)
            {
                this.temperatures.Add(i, temp);
                i += 3;
            }

            i = 1;
            this.humiditys = new Dictionary<int, double>();
            foreach (var hum in humiditys)
            {
                this.humiditys.Add(i, hum);
                i += 3;
            }

            i = 1;
            this.skys = new Dictionary<int, WeatherView.SkyCode>();
            foreach (var sky in skyStates)
            {
                this.skys.Add(i, sky);
                i += 3;
            }
        }

        private Dictionary<int, char> ConvertIcon(Dictionary<int, WeatherView.SkyCode> skyState)
        {
            Dictionary<int, char> icons = new Dictionary<int, char>();
            foreach (var item in skyState)
            {
                icons.Add(item.Key, WeatherIcon.GetWeatherIcon(item.Value));
            }
            return icons;
        }
    }
}
