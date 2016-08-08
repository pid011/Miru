using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.ViewModel.Clock
{
    /// <summary>
    /// 시간에 대한 속성을 제공합니다.
    /// </summary>
    interface IClock
    {
        /// <summary>
        /// 연도를 제공합니다.(yyyy)
        /// </summary>
        string Year { get; }
        /// <summary>
        /// 연중 몇번째 달인지를 제공합니다.(mm)
        /// </summary>
        string Month { get; }
        /// <summary>
        /// 날짜를 제공합니다.(dd)
        /// </summary>
        string Day { get; }
        /// <summary>
        /// 요일 이름을 제공합니다.(ddd)
        /// </summary>
        string Week { get; }
        /// <summary>
        /// 시간을 제공합니다.(hh)
        /// </summary>
        string Hour { get; }
        /// <summary>
        /// 분을 제공합니다.(nn)
        /// </summary>
        string Minute { get; }
        /// <summary>
        /// 오전 / 오후 중 하나를 제공합니다.(오전/오후)
        /// </summary>
        string AMPM { get; }
    }
}
