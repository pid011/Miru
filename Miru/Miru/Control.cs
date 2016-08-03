using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;

namespace Miru
{
    /// <summary>
    /// 초음파 센서에 대한 메서드, 속성을 제공합니다.
    /// </summary>
    public class Control
    {
        /// <summary>
        /// Gpio 핀 초기화 여부를 나타냅니다.
        /// </summary>
        public static bool IsInitialized { get; private set; } = false;
        /// <summary>
        /// 디바이스에서 사람까지의 측정될 거리범위를 cm단위로 나타냅니다.
        /// </summary>
        public static int Distance { get; set; } = 90;

        private static int PIN_ECHO = 23;
        private static int PIN_TRIG = 24;

        private static GpioController gpioController;
        private static GpioPin echoPin;
        private static GpioPin trigPin;


        /// <summary>
        /// Gpio 핀을 초기화합니다.
        /// </summary>
        public static  void InitializeGpio()
        {
            try
            {
                gpioController = GpioController.GetDefault();
                if (gpioController != null)
                {
                    echoPin = gpioController.OpenPin(PIN_ECHO);
                    echoPin.SetDriveMode(GpioPinDriveMode.Input);

                    trigPin = gpioController.OpenPin(PIN_TRIG);
                    trigPin.SetDriveMode(GpioPinDriveMode.Output);

                    IsInitialized = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static int GetDistance()
        {
            Stopwatch stopwatch = new Stopwatch();
            trigPin.Write(GpioPinValue.Low);
            Task.Delay(2);
            trigPin.Write(GpioPinValue.High);
            Task.Delay(20);
            trigPin.Write(GpioPinValue.Low);

            GpioPinValue value = GpioPinValue.Low;
            while (value == GpioPinValue.Low)
            {
                value = echoPin.Read();
            }
            var startTime = stopwatch.Elapsed.Milliseconds;

            while (value == GpioPinValue.High)
            {
                value = echoPin.Read();
            }
            var endTime = stopwatch.Elapsed.Milliseconds;

            stopwatch.Stop();

            double distance = (endTime - startTime) / 58;

            // 소수점 제거
            distance = Math.Round(distance);

            return (int) distance;
        }

        /// <summary>
        /// 초음파센서로 측정한 거리가 <see cref="Distance"/>에 설정된 거리보다 가까울때까지 기다립니다.
        /// </summary>
        public static async Task WaitDistanceAsync()
        {
            int currentDistance;
            do
            {
                currentDistance = GetDistance();
                await Task.Delay(500);
            } while (currentDistance < Distance);
        }
    }
}
