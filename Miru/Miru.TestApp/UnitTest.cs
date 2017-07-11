
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miru.Factory.Weather;
using Miru.Utils;
using System.Diagnostics;

namespace Miru.TestApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var weather = new WeatherFactory(71, 121, @"ZAPOSsSfE2OT%2F5L%2BOvgt9jjeNsTvt70ZX2uqpANGth%2B2%2BhGeA2RUx2dCXI3qEa3%2BCglP8AONokRPyfIkCET8zA%3D%3D");
            var result = weather.GetCurrentWeatherItem();

            Debug.Write("Completed!");
        }
    }
}
