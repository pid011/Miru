using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.Utils
{
    public class MiruConverter
    {
        public static string ConvertNumber(int target)
        {
            return target < 10 ? $"0{target}" : target.ToString();
        }

        public static int ConvertHour(int hour)
        {
            return hour > 12 ? hour - 12 : hour == 0 ? 12 : hour;
        }
    }
}
