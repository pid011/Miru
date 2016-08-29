using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.Util
{
    public class CommonUtil
    {
        public static string ConvertString(int target) => target < 10 ? $"0{target}" : target.ToString();

    }
}
