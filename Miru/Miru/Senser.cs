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
    public class UcSensor
    {
        /// <summary>
        /// TRIGGER핀의 Gpio 위치(Gpio xx)
        /// </summary>
        public const int PIN_TRIGGER = 23;
        /// <summary>
        /// ECHO핀의 Gpio 위치
        /// </summary>
        public const int PIN_ECHO = 18;

        /// <summary>
        /// 초음파센서에 대한 메서드를 제공합니다.
        /// </summary>
        public static UcSensor senser = new UcSensor();

        GpioController gpio;

        GpioPin triggerPin;
        GpioPin echoPin;

        private UcSensor()
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
            mre.WaitOne(500);
            Stopwatch pulseLength = new Stopwatch();

            triggerPin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            triggerPin.Write(GpioPinValue.Low);

            while (echoPin.Read() == GpioPinValue.Low)
            {
            }
            pulseLength.Start();


            while (echoPin.Read() == GpioPinValue.High)
            {
            }
            pulseLength.Stop();

            TimeSpan timeBetween = pulseLength.Elapsed;
            Debug.WriteLine(timeBetween.ToString());
            double distance = timeBetween.TotalSeconds * 17000;
            Debug.WriteLine($"Current distance: {distance}cm");

            return Math.Round(distance);
        }

    }
}