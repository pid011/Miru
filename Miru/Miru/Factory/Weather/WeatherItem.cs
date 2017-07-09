using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.Factory.Weather
{
    /// <summary>
    /// 날씨 정보가 담겨져 있습니다.
    /// </summary>
    public class WeatherItem
    {
        /// <summary>
        /// 강수확률
        /// </summary>
        public class POP
        {
            public int Value { get; set; }
            public const string Unit = "%";
        }

        /// <summary>
        /// 하늘 상태
        /// </summary>
        public class SkyStat
        {
            public Sky.SkyCode Value { get; set; }
        }

        /// <summary>
        /// 6시간 강수량
        /// </summary>
        public class R06
        {
            public double Value { get; set; }
            public const string Unit = "mm";
        }

        /// <summary>
        /// 습도
        /// </summary>
        public class REH
        {
            public int Value { get; set; }
            public const string Unit = "%";
        }

        /// <summary>
        /// 6시간 신적설
        /// </summary>
        public class S06
        {
            public int Value { get; set; }
            public const string Unit = "cm";
        }

        /// <summary>
        /// 3시간 기온
        /// </summary>
        public class T3H
        {
            public double Value { get; set; }
            public const string Unit = "℃";
        }

        /// <summary>
        /// 아침 최저기온
        /// </summary>
        public class TMN
        {
            public double Value { get; set; }
            public const string Unit = "℃";
        }

        /// <summary>
        /// 낮 최고기온
        /// </summary>
        public class TMX
        {
            public double Value { get; set; }
            public const string Unit = "℃";
        }

        /// <summary>
        /// 풍속
        /// </summary>
        public class WSD
        {
            public double Value { get; set; }
            public const string Unit = "㎧";
        }
    }
}
