﻿using Miru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Miru.Factory.Weather.Sky;

namespace Miru.Factory.Weather
{
    public class WeatherConverter
    {

        /// <summary>
        /// <see cref="SkyCode"/>를 <see cref="string"/>형태로 변환합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ConvertSkyCodeToString(SkyCode target)
        {
            string converted = "idk";

            switch (target)
            {
                case SkyCode.Sunny:
                    converted = "맑음";
                    break;
                case SkyCode.PartlyCloudy:
                    converted = "구름 조금";
                    break;
                case SkyCode.MostlyCloudy:
                    converted = "구름 많음";
                    break;
                case SkyCode.Fog:
                    converted = "흐림";
                    break;
                case SkyCode.Rain:
                    converted = "비";
                    break;
                case SkyCode.Snow:
                    converted = "눈";
                    break;
                case SkyCode.Drizzle:
                    converted = "비 또는 눈";
                    break;
                default:
                    break;
            }

            return converted;
        }

        public static Dictionary<string, string> ConvertBaseDateTime(DateTime time)
        {
            int hour = time.Hour;
            int min = time.Minute;
            var baseList = new List<int>() { 2, 5, 8, 11, 14, 17, 20, 23 };
            int baseResult = 0;

            if (hour == baseList[7] || hour < baseList[0])
            {
                baseResult = baseList[7];
                if (hour == baseList[7])
                {
                    if (min < 11)
                    {
                        baseResult = baseList[6];
                    }
                }
            }
            else if (hour >= baseList[0] && hour < baseList[1])
            {
                baseResult = baseList[0];
                if (hour == baseList[0])
                {
                    if (min < 11)
                    {
                        baseResult = baseList[7];
                    }
                }
            }
            else
            {
                for (int i = 1; i < baseList.Count; i++)
                {
                    if (hour >= baseList[i] && hour < baseList[i + 1])
                    {
                        baseResult = baseList[i];
                        if (hour == baseList[i])
                        {
                            if (min < 11)
                            {
                                baseResult = baseList[i - 1];
                            }
                        }
                    }
                }
            }
            if (hour < baseList[0])
            {
                time = time.Subtract(TimeSpan.FromDays(1));
            }
            var baseDate = $"{time.Year}{MiruConverter.ConvertNumber(time.Month)}{MiruConverter.ConvertNumber(time.Day)}";
            var baseTime = MiruConverter.ConvertNumber(baseResult) + "00";

            return new Dictionary<string, string>
            {
                ["baseDate"] = baseDate,
                ["baseTime"] = baseTime
            };
        }

        public static DateTime ConvertDateTime(string date, string time)
        {
            var year = date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString();
            var month = date[4].ToString() + date[5].ToString();
            var day = date[6].ToString() + date[7].ToString();
            var hour = time[0].ToString() + time[1].ToString();
            var result = new DateTime(
                Convert.ToInt32(year),
                Convert.ToInt32(month),
                Convert.ToInt32(day),
                Convert.ToInt32(hour),
                0, 0);

            return result;
        }
    }
}
