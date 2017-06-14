using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Miru.Utils
{
    public class StringManager
    {
        private static ResourceLoader loader = new ResourceLoader();

        public static string GetString(string keyword)
        {
            return loader.GetString(keyword) ?? string.Empty;
        }
    }
}
