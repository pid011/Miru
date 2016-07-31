using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Miru.GpioPin
{
    public class PIRSenser : Senser
    {
        private const int PIR_SENSER_PIN = 23;
        private Windows.Devices.Gpio.GpioPin pin;

        /// <summary>
        /// PIRSenser를 읽어들입니다. 실패했을시 false를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public override bool InitGpio()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                this.pin = null;
                return false;
            }

            this.pin = gpio.OpenPin(PIR_SENSER_PIN);
            this.pin.SetDriveMode(GpioPinDriveMode.Input);
            return true;
        }
    }
}
