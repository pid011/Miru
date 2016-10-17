using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Devices.Gpio;

namespace Miru
{
    public class Senser
    {
        /// <summary>
        /// TRIGGER핀의 Gpio 위치
        /// </summary>
        public const int PIN_TRIGGER = 23;
        /// <summary>
        /// ECHO핀의 Gpio 위치
        /// </summary>
        public const int PIN_ECHO = 18;

        /// <summary>
        /// 거리 측정에 대한 메서드를 제공합니다.
        /// </summary>
        public static Senser Distance { get; } = new Senser();

        GpioController gpio;

        GpioPin triggerPin;
        GpioPin echoPin;

        private Senser()
        {
            gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                return;
            }

            triggerPin = gpio.OpenPin(PIN_TRIGGER);
            triggerPin.SetDriveMode(GpioPinDriveMode.Output);

            echoPin = gpio.OpenPin(PIN_ECHO);
            echoPin.SetDriveMode(GpioPinDriveMode.Input);
            triggerPin.Write(GpioPinValue.Low);
        }

        /// <summary>
        /// 현재 거리를 측정하여 소수점을 자른 정수형으로 반환합니다.
        /// </summary>
        /// <returns>현재 거리</returns>
        public double GetDistance()
        {
            ManualResetEvent mre = new ManualResetEvent(false);

            Stopwatch stopwatch = new Stopwatch();

            this.triggerPin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            this.triggerPin.Write(GpioPinValue.Low);

            while (this.echoPin.Read() == GpioPinValue.Low)
            {
            }
            stopwatch.Start();

            while (this.echoPin.Read() == GpioPinValue.High)
            {
            }
            stopwatch.Stop();

            TimeSpan timeBetween = stopwatch.Elapsed;

            double distance = timeBetween.TotalSeconds * 17000;
            Debug.WriteLine($"{DateTime.Now.ToString()}  Distance: {distance}");

            return Math.Round(distance);
        }

    }
}