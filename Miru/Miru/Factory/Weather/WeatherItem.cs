using Miru.Utils;
using System;

namespace Miru.Factory.Weather
{
    /// <summary>
    /// 날씨 정보가 담겨져 있습니다.
    /// </summary>
    public class WeatherItem
    {
        public DateTime BaseDateTime { get; set; }
        public string BaseHour => MiruConverter.ConvertNumber(BaseDateTime.Hour);
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

        public class POPSet : BaseSet<int>
        {
            public POPSet() : base("%")
            {
            }
        }

        public class SkyStatSet
        {
            /// <summary>
            /// 값
            /// </summary>
            public Sky.SkyCode Value { get; set; }

            /// <summary>
            /// 아이콘
            /// </summary>
            public string Icon { get; set; }
        }

        public class R06Set : BaseSet<double>
        {
            public R06Set() : base("mm")
            {
            }
        }

        public class REHSet : BaseSet<double>
        {
            public REHSet() : base("%")
            {
            }
        }

        public class S06Set : BaseSet<int>
        {
            public S06Set() : base("cm")
            {
            }
        }

        public class T3HSet : BaseSet<double>
        {
            public T3HSet() : base("℃")
            {
            }
        }

        public class WSDSet : BaseSet<double>
        {
            public WSDSet() : base("㎧")
            {
            }
        }

        public class BaseSet<T>
        {
            public BaseSet(string unit)
            {
                Unit = unit;
            }

            /// <summary>
            /// 값
            /// </summary>
            public T Value { get; set; }

            /// <summary>
            /// 단위
            /// </summary>
            public string Unit { get; }

            /// <summary>
            /// 값을 단위와 합쳐 문자열로 반환합니다.
            /// </summary>
            public string ValueWithUnit => Value.ToString() + Unit;
        }
    }
}