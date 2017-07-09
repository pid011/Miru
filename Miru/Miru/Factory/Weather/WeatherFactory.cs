using Miru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace Miru.Factory.Weather
{
    public class WeatherFactory
    {
        public double Nx { get; private set; }
        public double Ny { get; private set; }

        private readonly string apiKey;

        public WeatherFactory(double nx, double ny, string apiKey)
        {
            SetNyNx(nx, ny);
            this.apiKey = apiKey;
        }

        public void SetNyNx(double nx, double ny)
        {
            Ny = ny;
            Nx = nx;
        }

        public WeatherItem GetCurrentWeatherItem()
        {
            var now = DateTime.Now.Date;
            var baseDate = $"{now.Year}{MiruConverter.ConvertNumber(now.Month)}{MiruConverter.ConvertNumber(now.Day)}";
            var baseTime = WeatherConverter.ConvertBaseTime(DateTime.Now);

            var jsonData = RequestData(baseDate, baseTime);

            return new WeatherItem();
        }

        public string RequestData(string baseDate, string baseTime)
        {
            string uri = @"http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastSpaceData";

            var dataParams = new StringBuilder();
            dataParams.Append($@"?ServiceKey={apiKey}");
            dataParams.Append($@"&base_date={baseDate}");
            dataParams.Append($@"&base_time={baseTime}");
            dataParams.Append($@"&nx={Nx}");
            dataParams.Append($@"&ny={Ny}");
            dataParams.Append($@"&_type=json");
            dataParams.Append($@"&numOfRows=62");

            string result = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage response = client.GetAsync(dataParams.ToString()).Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }
    }
}
