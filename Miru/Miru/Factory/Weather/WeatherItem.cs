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
        public POP POPValue { get; set; } = new POP();

        /// <summary>
        /// 하늘 상태
        /// </summary>
        public SkyStat SkyStatValue { get; set; } = new SkyStat();

        /// <summary>
        /// 6시간 강수량
        /// </summary>
        public R06 R06Value { get; set; } = new R06();

        /// <summary>
        /// 습도
        /// </summary>
        public REH REHValue { get; set; } = new REH();

        /// <summary>
        /// 6시간 신적설
        /// </summary>
        public S06 S06Value { get; set; } = new S06();

        /// <summary>
        /// 3시간 기온
        /// </summary>
        public T3H T3HValue { get; set; } = new T3H();

        /// <summary>
        /// 풍속
        /// </summary>
        public WSD WSDValue { get; set; } = new WSD();

        public class POP
        {
            public int Value { get; set; }
            public readonly string Unit = "%";
        }

        public class SkyStat
        {
            public Sky.SkyCode Value { get; set; }
        }

        public class R06
        {
            public double Value { get; set; }
            public readonly string Unit = "mm";
        }

        public class REH
        {
            public int Value { get; set; }
            public readonly string Unit = "%";
        }

        public class S06
        {
            public int Value { get; set; }
            public readonly string Unit = "cm";
        }

        public class T3H
        {
            public double Value { get; set; }
            public readonly string Unit = "℃";
        }

        public class WSD
        {
            public double Value { get; set; }
            public readonly string Unit = "㎧";
        }
    }
}