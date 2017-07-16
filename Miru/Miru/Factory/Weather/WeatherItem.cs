using System;

namespace Miru.Factory.Weather
{
    /// <summary>
    /// 날씨 정보가 담겨져 있습니다.
    /// </summary>
    public class WeatherItem
    {
        public DateTime BaseDateTime { get; set; }
        public bool Success { get; set; }

        /// <summary>
        /// 강수확률
        /// </summary>
        public POPSet POP { get; set; } = new POPSet();

        /// <summary>
        /// 하늘 상태
        /// </summary>
        public SkyStatSet SkyStat { get; set; } = new SkyStatSet();

        /// <summary>
        /// 6시간 강수량
        /// </summary>
        public R06Set R06 { get; set; } = new R06Set();

        /// <summary>
        /// 습도
        /// </summary>
        public REHSet REH { get; set; } = new REHSet();

        /// <summary>
        /// 6시간 신적설
        /// </summary>
        public S06Set S06 { get; set; } = new S06Set();

        /// <summary>
        /// 3시간 기온
        /// </summary>
        public T3HSet T3H { get; set; } = new T3HSet();

        /// <summary>
        /// 풍속
        /// </summary>
        public WSDSet WSD { get; set; } = new WSDSet();

        public class POPSet
        {
            public int Value { get; set; }
            public readonly string Unit = "%";
        }

        public class SkyStatSet
        {
            public Sky.SkyCode Value { get; set; }
            public string Icon { get; set; }
        }

        public class R06Set
        {
            public double Value { get; set; }
            public readonly string Unit = "mm";
        }

        public class REHSet
        {
            public int Value { get; set; }
            public readonly string Unit = "%";
        }

        public class S06Set
        {
            public int Value { get; set; }
            public readonly string Unit = "cm";
        }

        public class T3HSet
        {
            public double Value { get; set; }
            public readonly string Unit = "℃";
        }

        public class WSDSet
        {
            public double Value { get; set; }
            public readonly string Unit = "㎧";
        }
    }
}