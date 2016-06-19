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
		/// 제공 기능
		/// </summary>
		public enum ScopeType
		{
			/// <summary>
			/// 현재날씨(분별)
			/// </summary>
			Current_Time,
			/// <summary>
			/// 단기예보
			/// </summary>
			ShortTerm_Forecast,
			/// <summary>
			/// 중기예보
			/// </summary>
			MidTerm_Forecasts
		}
		/// <summary>
		/// 하늘상태코드
		/// </summary>
		public enum SkyType
		{
			/// <summary>
			/// 맑음
			/// </summary>
			SKY_O01,
			/// <summary>
			/// 구름조금
			/// </summary>
			SKY_O02,
			/// <summary>
			/// 구름많음
			/// </summary>
			SKY_O03,
			/// <summary>
			/// 구름많고 비
			/// </summary>
			SKY_O04,
			/// <summary>
			/// 구름많고 눈
			/// </summary>
			SKY_O05,
			/// <summary>
			/// 구름많고 비 또는 눈
			/// </summary>
			SKY_O06,
			/// <summary>
			/// 흐림
			/// </summary>
			SKY_O07,
			/// <summary>
			/// 흐리고 비
			/// </summary>
			SKY_O08,
			/// <summary>
			/// 흐리고 눈
			/// </summary>
			SKY_O09,
			/// <summary>
			/// 흐리고 비 또는 눈
			/// </summary>
			SKY_O10,
			/// <summary>
			/// 흐리고 낙뢰
			/// </summary>
			SKY_O11,
			/// <summary>
			/// 뇌우, 비
			/// </summary>
			SKY_O12,
			/// <summary>
			/// 뇌우, 눈
			/// </summary>
			SKY_O13,
			/// <summary>
			/// 뇌우, 비 또는 눈
			/// </summary>
			SKY_O14
		}

		private int version { get; set; }
		public double lat { get; private set; }
		public double lon { get; private set; }
		private string appKey { get; set; }
		public ScopeType scopeType { get; private set; }

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
		public string SkyCode { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="url">Weather Plenet API url</param>
		/// <param name="version">API 버전</param>
		/// <param name="lat">위도(Only Korea)</param>
		/// <param name="lon">경도(Only Korea)</param>
		/// <param name="appKey">SK plenet에서 제공받은 appKey</param>
		/// <param name="scopeType">조회할 날씨정보 타입</param>
		public WeatherInfo(ScopeType scopeType, int version, double lat, double lon, string appKey)
		{
			this.scopeType = scopeType;
			this.version = version;
			this.lat = lat;
			this.lon = lon;
			this.appKey = appKey;
		}

		/// <summary>
		/// 날씨정보를 생성합니다.
		/// </summary>
		public async Task Create()
		{
			string json = await Request_WeatherJson();
			JsonTOWeatherInfo(json);
		}

		private async Task<string> Request_WeatherJson()
		{
			string scopeUrl = string.Empty;
			switch(scopeType)
			{
				case ScopeType.Current_Time:
					scopeUrl = "http://apis.skplanetx.com/weather/current/minutely";
					break;
				case ScopeType.ShortTerm_Forecast:
					scopeUrl = "http://apis.skplanetx.com/weather/forecast/3days";
					break;
				case ScopeType.MidTerm_Forecasts:
					scopeUrl = "http://apis.skplanetx.com/weather/forecast/6days";
					break;
				default:
					break;
			}

			string url = $"{scopeUrl}?version={version}&lat={lat}&lon={lon}&appKey={appKey}";

			HttpClient client = new HttpClient();
			Task<string> getStringTask = client.GetStringAsync(url);
			string json = await getStringTask;

			return json;
		}

		private void JsonTOWeatherInfo(string json)
		{
			try
			{
				JObject obj = JObject.Parse(json);
				JArray arr = JArray.Parse(obj["weather"]["minutely"].ToString());

				foreach(var item in arr)
				{
					Temperature = Convert.ToDouble(item["temperature"]["tc"].ToString());
					Humidiy = Convert.ToDouble(item["humidity"].ToString());
					SkyCode = item["sky"]["code"].ToString();
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

	}
}
