using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Miru.Util;
using Windows.Devices.Gpio;

namespace Miru
{
    /// <summary>
    /// 초음파 센서에 대한 메서드, 속성을 제공합니다.
    /// </summary>
    public class Senser
    {
        /// <summary>
        /// Gpio 핀 초기화 여부를 나타냅니다.
        /// </summary>
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// 디바이스에서 사람까지의 측정될 거리범위를 cm단위로 나타냅니다.
        /// </summary>
        public long Distance { get; set; } = 90;

        private int PIN_ECHO = 23;
        private int PIN_TRIG = 24;

        private GpioController gpioController;
        private GpioPin echoPin;
        private GpioPin trigPin;

        /// <summary>
        /// Gpio 핀을 초기화합니다.
        /// </summary>
        public void InitializeGpio()
        {
            gpioController = GpioController.GetDefault();
            if (gpioController != null)
            {
                GpioOpenStatus openStatus = GpioOpenStatus.PinOpened;
                bool isEchoPinOpend = gpioController.TryOpenPin(PIN_ECHO, GpioSharingMode.SharedReadOnly, out echoPin, out openStatus);
                if(!isEchoPinOpend)
                {
                    throw new NullReferenceException(ResourcesString.GetString("pin_echo_null_error"));
                }
                // echoPin = gpioController.OpenPin(PIN_ECHO);
                // echoPin.SetDriveMode(GpioPinDriveMode.Input);

                bool isTrigPinOpend = gpioController.TryOpenPin(PIN_TRIG, GpioSharingMode.Exclusive, out trigPin, out openStatus);
                if (!isTrigPinOpend)
                {
                    throw new NullReferenceException(ResourcesString.GetString("pin_trig_null_error"));
                }
                // trigPin = gpioController.OpenPin(PIN_TRIG);
                // trigPin.SetDriveMode(GpioPinDriveMode.Output);

                IsInitialized = true;
            }
        }

        public long GetDistance()
        {
            Stopwatch stopwatch = new Stopwatch();
            trigPin.Write(GpioPinValue.Low);
            Task.Delay(2);
            trigPin.Write(GpioPinValue.High);
            Task.Delay(20);
            trigPin.Write(GpioPinValue.Low);

            while (echoPin.Read() == GpioPinValue.Low)
                ;

            var startTime = stopwatch.ElapsedMilliseconds;

            while (echoPin.Read() == GpioPinValue.High)
                ;

            var endTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Stop();

            var distance = (endTime - startTime) / 58;

            return distance;
        }
    }
}