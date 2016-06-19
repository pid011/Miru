using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
			/// ERROR
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
		public double Temperature { get; private set; }
		/// <summary>
		/// 습도
		/// </summary>
		public double Humidiy { get; private set; }
		/// <summary>
		/// 현재 하늘상태
		/// </summary>
		public SkyType SkyCode { get; private set; }

		private string CurrentWeatherJson = string.Empty;
		// private string ForecastWeatherJson = string.Empty;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="version">API 버전</param>
		/// <param name="lat">위도(Only Korea)</param>
		/// <param name="lon">경도(Only Korea)</param>
		/// <param name="appKey">SK plenet에서 제공받은 appKey</param>
		public WeatherInfo(int version, double lat, double lon, string appKey)
		{
			this.version = version;
			this.lat = lat;
			this.lon = lon;
			this.appKey = appKey;
		}

		/// <summary>
		/// 날씨정보를 생성합니다.
		/// </summary>
		public async void Create()
		{
			await Request_WeatherJson();
			JsonTOWeatherInfo(CurrentWeatherJson);
		}

		private async Task Request_WeatherJson()
		{
			string scopeUrl = string.Empty;
			scopeUrl = "http://apis.skplanetx.com/weather/current/minutely";

			string url = $"{scopeUrl}?version={version}&lat={lat}&lon={lon}&appKey={appKey}";

			HttpClient client = new HttpClient();
			Task<string> getStringTask = client.GetStringAsync(url);
			CurrentWeatherJson = await getStringTask;
		}

		private void JsonTOWeatherInfo(string json)
		{
				JObject obj = JObject.Parse(json);
				JArray arr = JArray.Parse(obj["weather"]["minutely"].ToString());

				foreach(var item in arr)
				{
					Temperature = Convert.ToDouble(item["temperature"]["tc"].ToString());

					Humidiy = Convert.ToDouble(item["humidity"].ToString());

					string skycode = item["sky"]["code"].ToString();
					SkyCode = Enum.IsDefined(typeof(SkyType), skycode)
						? (SkyType)Enum.Parse(typeof(SkyType), skycode) : SkyType.NULL;
			}
		}

	}
}
