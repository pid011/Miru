using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Miru.Util
{
    /// <summary>
    /// 문자열 리소스에 대한 메서드를 제공합니다.
    /// </summary>
    public class ResourcesString
    {
        private static ResourceLoader resourceLoader = new ResourceLoader();

        /// <summary>
        /// 입력한 매개변수와 일치하는 이름의 문자열을 반환합니다.
        /// </summary>
        /// <param name="name">반환할 문자열 리소스의 이름입니다.</param>
        /// <returns>입력한 리소스이름의 문자열</returns>
        public static string GetString(string name) => resourceLoader.GetString(name);
    }
}
