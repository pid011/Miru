using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.Util
{
    //TODO: Config 파일화
    public static class Config
    {
        /// <summary>
        /// 날씨 관련 설정
        /// </summary>
        public class Weather
        {
            // weather api data

            public static readonly int version = 1;

            //대한민국 경기도 여주시 홍문동
            public static readonly double lat = 37.285944;
            public static readonly double lon = 127.636764;

            /// <summary>
            /// Miru의 appKey
            /// </summary>
            public static readonly string appKey = "5424eae1-8e98-3d89-82e5-e9a1c589a7ba";
        }

        /// <summary>
        /// 센서 관련 설정
        /// </summary>
        public class Senser
        {
            public static readonly double distance = 40;
        }
    }
}
