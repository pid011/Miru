using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Miru.Widget
{
	/// <summary>
	/// SK Weather Plenet API를 활용한 날씨 정보를 제공합니다.
	/// </summary>
	public class WeatherInfo
	{
		/// <summary>
		/// 하늘상태코드
		/// </summary>
		public enum SkyType
		{
			/// <summary>
			/// 맑음
			/// </summary>
			SKY_A01,

			/// <summary>
			/// 구름조금
			/// </summary>
			SKY_A02,

			/// <summary>
			/// 구름많음
			/// </summary>
			SKY_A03,

			/// <summary>
			/// 구름많고 비
			/// </summary>
			SKY_A04,

			/// <summary>
			/// 구름많고 눈
			/// </summary>
			SKY_A05,

			/// <summary>
			/// 구름많고 비 또는 눈
			/// </summary>
			SKY_A06,

			/// <summary>
			/// 흐림
			/// </summary>
			SKY_A07,

			/// <summary>
			/// 흐리고 비
			/// </summary>
			SKY_A08,

			/// <summary>
			/// 흐리고 눈
			/// </summary>
			SKY_A09,

			/// <summary>
			/// 흐리고 비 또는 눈
			/// </summary>
			SKY_A10,

			/// <summary>
			/// 흐리고 낙뢰
			/// </summary>
			SKY_A11,

			/// <summary>
			/// 뇌우, 비
			/// </summary>
			SKY_A12,

			/// <summary>
			/// 뇌우, 눈
			/// </summary>
			SKY_A13,

			/// <summary>
			/// 뇌우, 비 또는 눈
			/// </summary>
			SKY_A14,

			/// <summary>
			/// error parse
			/// </summary>
			NULL
		}

		private int version { get; set; }
		public double lat { get; private set; }
		public double lon { get; private set; }
		private string appKey { get; set; }

		/// <summary>
		/// 온도
		/// </summary>
		public Dictionary<string, double> Temperature { get; private set; }

		/// <summary>
		/// 습도
		/// </summary>
		public Dictionary<string, double> Humidiy { get; private set; }

		/// <summary>
		/// 현재 하늘상태
		/// </summary>
		public Dictionary<string, SkyType> SkyCode { get; private set; }

		/// <summary>
		/// 날씨정보 로드 성공여부
		/// </summary>
		public bool IsSuccessful { get; private set; } = true;

		private List<string> scopeUrl;

		/// <summary>
		/// 날씨정보 조회에 필요한 매개변수를 정합니다.
		/// </summary>
		/// <param name="version">API 버전</param>
		/// <param name="lat">위도(Only Korea)</param>
		/// <param name="lon">경도(Only Korea)</param>
		/// <param name="appKey">SK plenet에서 제공받은 appKey</param>
		/// <param name="scopeType">조회할 날씨정보 타입</param>
		public WeatherInfo(int version, double lat, double lon, string appKey)
		{
			this.version = version;
			this.lat = lat;
			this.lon = lon;
			this.appKey = appKey;
			scopeUrl = new List<string>
			{
				"http://apis.skplanetx.com/weather/current/minutely",
				"http://apis.skplanetx.com/weather/forecast/3days"
			};
		}

		/// <summary>
		/// 날씨정보를 생성합니다.
		/// </summary>
		public async void Create()
		{
			string[] json = new string[1];
			json = await Request_WeatherJson();
		}

		private async Task<string[]> Request_WeatherJson()
		{
			string[] json = new string[1];
			string url1 = $"{scopeUrl[0]}?version={version}&lat={lat}&lon={lon}&appKey={appKey}";
			HttpClient client1 = new HttpClient();
			Task<string> getStringTask1 = client1.GetStringAsync(url1);
			json[0] = await getStringTask1;

			string url2 = $"{scopeUrl[1]}?version={version}&lat={lat}&lon={lon}&appKey={appKey}";
			HttpClient client2 = new HttpClient();
			Task<string> getStringTask2 = client2.GetStringAsync(url2);
			json[1] = await getStringTask2;

			return json;
		}

		private void Parse_json(string CurrentWeatherJson, string ForecastWeatherJson)
		{
			try
			{
				JObject obj = JObject.Parse(CurrentWeatherJson);
				JArray arr = JArray.Parse(obj["weather"]["minutely"].ToString());

				foreach(var item in arr)
				{
					Temperature.Add("Current", Convert.ToDouble(item["temperature"]["tc"].ToString()));

					Humidiy.Add("Current", Convert.ToDouble(item["humidity"].ToString()));

					string skycode = item["sky"]["code"].ToString();
					SkyCode.Add("Current",
						(Enum.IsDefined(typeof(SkyType), skycode)
						? (SkyType)Enum.Parse(typeof(SkyType), skycode) : SkyType.NULL));
				}
			}
			catch(Exception)
			{
				Temperature.Add("Current", 0);
				Humidiy.Add("Current", 0);
				SkyCode.Add("Current", SkyType.NULL);
			}
		}
	}
}