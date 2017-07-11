using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Miru.Factory.Weather
{
    public class WeatherFactory
    {
        public double Nx { get; private set; }
        public double Ny { get; private set; }

        private readonly string apiKey;

        public WeatherFactory(double nx, double ny, string apiKey)
        {
            SetNxNy(nx, ny);
            this.apiKey = apiKey;
        }

        public void SetNxNy(double nx, double ny)
        {
            Ny = ny;
            Nx = nx;
        }

        public List<WeatherItem> GetCurrentWeatherItem()
        {
            var baseDateTime = WeatherConverter.ConvertBaseDateTime(DateTime.Now);

            var jsonData = RequestData(baseDateTime["baseDate"], baseDateTime["baseTime"]);
            return JsonParser(jsonData);
        }

        private string RequestData(string baseDate, string baseTime)
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

        private List<WeatherItem> JsonParser(string json)
        {
            var items = new List<WeatherItem>();

            var obj = JObject.Parse(json);
            obj = (JObject)obj["response"];
            if ((string)obj["header"]["resultCode"] != "0000")
            {
                items.Add(new WeatherItem() { Success = false });
                return items;
            }

            var dataArray = (JArray)obj["body"]["items"]["item"];

            var dataSet = new List<WeatherData>();
            foreach (var data in dataArray)
            {
                dataSet.Add(new WeatherData()
                {
                    Category = (string)data["category"],
                    FcstDate = (string)data["fcstDate"],
                    FcstTime = (string)data["fcstTime"],
                    FcstValue = (string)data["fcstValue"]
                });
            }

            var sortedData = new List<List<WeatherData>>();
            while (dataSet.Count != 0)
            {
                var dataSetA = dataSet.Where(x => x.FcstTime == dataSet.First().FcstTime).ToList();
                dataSetA.RemoveAll(x => x.FcstDate != dataSetA.First().FcstDate);

                for (int i = 0; i < dataSetA.Count; i++)
                {
                    dataSet.Remove(dataSetA[i]);
                }

                sortedData.Add(dataSetA);
            }

            if (sortedData.Count > 6)
            {
                sortedData.RemoveRange(6, sortedData.Count - 6);
            }

            foreach (var data in sortedData)
            {
                var result = new WeatherItem()
                {
                    Success = true,
                    BaseDateTime = WeatherConverter.ConvertDateTime(data.First().FcstDate, data.First().FcstTime)
                };

                int pty = 0;
                int sky = 0;
                foreach (var item in data)
                {
                    switch (item.Category)
                    {
                        case "POP":
                            result.POPValue.Value = Convert.ToInt32(item.FcstValue);
                            break;

                        case "R06":
                            result.R06Value.Value = Convert.ToDouble(item.FcstValue);
                            break;

                        case "REH":
                            result.REHValue.Value = Convert.ToInt32(item.FcstValue);
                            break;

                        case "S06":
                            result.S06Value.Value = Convert.ToInt32(item.FcstValue);
                            break;

                        case "T3H":
                            result.T3HValue.Value = Convert.ToDouble(item.FcstValue);
                            break;

                        case "WSD":
                            result.WSDValue.Value = Convert.ToDouble(item.FcstValue);
                            break;

                        case "PTY":
                            pty = Convert.ToInt32(item.FcstValue);
                            break;

                        case "SKY":
                            sky = Convert.ToInt32(item.FcstValue);
                            break;

                        default:
                            break;
                    }
                }

                result.SkyStatValue.Value = WeatherConverter.ConvertSky(sky, pty);

                items.Add(result);
            }

            return items;
        }

        internal class WeatherData
        {
            public string Category { get; set; }
            public string FcstDate { get; set; }
            public string FcstTime { get; set; }
            public string FcstValue { get; set; }
        }
    }
}