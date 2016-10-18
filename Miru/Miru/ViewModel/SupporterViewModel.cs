using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miru.Factory;
using static Miru.ViewModel.WeatherViewModel;

namespace Miru.ViewModel
{
    public class SupporterViewModel : BindableBase, IDisposable
    {
        private Supporter supporter;

        private string answer;

        public string Answer
        {
            get
            {
                return this.answer;
            }
            set
            {
                this.SetProperty(ref this.answer, value);
            }
        }

        public SupporterViewModel()
        {
            this.supporter = new Supporter();

            this.Answer = this.supporter.GetAnswer();
        }


        public void Dispose()
        {

        }
    }
}
