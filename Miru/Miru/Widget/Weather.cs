using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Linq;

namespace Miru.Widget.Weather
{
	/// <summary>
	/// SK Weather Plenet API를 활용한 날씨 정보를 제공합니다.
	/// </summary>
	public class Weather
	{
		/// <summary>
		/// 하늘상태코드
		/// </summary>
		public enum SkyType
		{
			/// <summary>
			/// 맑음
			/// </summary>
			SKY_01,

			/// <summary>
			/// 구름조금
			/// </summary>
			SKY_02,

			/// <summary>
			/// 구름많음
			/// </summary>
			SKY_03,

			/// <summary>
			/// 구름많고 비
			/// </summary>
			SKY_04,

			/// <summary>
			/// 구름많고 눈
			/// </summary>
			SKY_05,

			/// <summary>
			/// 구름많고 비 또는 눈
			/// </summary>
			SKY_06,

			/// <summary>
			/// 흐림
			/// </summary>
			SKY_07,

			/// <summary>
			/// 흐리고 비
			/// </summary>
			SKY_08,

			/// <summary>
			/// 흐리고 눈
			/// </summary>
			SKY_09,

			/// <summary>
			/// 흐리고 비 또는 눈
			/// </summary>
			SKY_10,
			
			/// <summary>
			/// 흐리고 낙뢰
			/// </summary>
			SKY_11,

			/// <summary>
			/// 뇌우, 비
			/// </summary>
			SKY_12,

			/// <summary>
			/// 뇌우, 눈
			/// </summary>
			SKY_13,

			/// <summary>
			/// 뇌우, 비 또는 눈
			/// </summary>
			SKY_14,

			/// <summary>
			/// ERROR
			/// </summary>
			NOTHING
		}

		/// <summary>
		/// 온도(3시간 단위)
		/// </summary>
		public Dictionary<int, double> Temperature { get; protected set; }

		/// <summary>
		/// 습도(3시간 단위)
		/// </summary>
		public Dictionary<int, double> Humidiy { get; protected set; }

		/// <summary>
		/// 현재 하늘상태(3시간 단위)
		/// </summary>
		public Dictionary<int, SkyType> SkyCode { get; protected set; }

		/// <summary>
		/// 에러확인
		/// </summary>
		public bool	IsError { get; protected set; } = false;
		/// <summary>
		/// 에러 메시지
		/// </summary>
		public string ErrorMsg { get; protected set; }
	}

	/// <summary>
	/// 날씨정보생성을 도와줍니다.
	/// </summary>
	public class WeatherUtil : Weather
	{
		private int		version;
		private double	lat;
		private double	lon;
		private string	appKey;

		/// <summary>
		/// 날씨정보를 생성합니다.
		/// </summary>
		/// <param name="version">API 버전</param>
		/// <param name="lat">위도(Only Korea)</param>
		/// <param name="lon">경도(Only Korea)</param>
		/// <param name="appKey">SK plenet에서 제공받은 appKey</param>
		public WeatherUtil(int version, double lat, double lon, string appKey)
		{
			this.version = version;
			this.lat = lat;
			this.lon = lon;
			this.appKey = appKey;
		}

		async Task<string> RequestJsonAsync(string url, HttpClient client)
			=> await client.GetStringAsync(url);

		public async Task<Weather> RequestWeatherAsync()
		{
			string CurrentWeatherUrl = "http://apis.skplanetx.com/weather/current/minutely";
			string ForecastWeatherUrl = "http://apis.skplanetx.com/weather/forecast/3days";

			CurrentWeatherUrl = $"{CurrentWeatherUrl}?version={version}&lat={lat}&lon={lon}&appKey={appKey}";
			ForecastWeatherUrl = $"{ForecastWeatherUrl}?version={version}&lat={lat}&lon={lon}&appKey={appKey}";

			HttpClient client = new HttpClient();
			try
			{
				Task<string> getStringTask1 = RequestJsonAsync(CurrentWeatherUrl, client);
				Task<string> getStringTask2 = RequestJsonAsync(ForecastWeatherUrl, client);
				string json1 = await getStringTask1;
				string json2 = await getStringTask2;
				CurrentWeatherJsonParse(json1);
				ForecastWeatherJsonFarse(json2);
			}
			catch(HttpRequestException e)
			{
				IsError = true;
				ErrorMsg = e.Message;
			}

			return this;
		}

		private void CurrentWeatherJsonParse(string CurrentWeatherJson)
		{
			JObject obj = JObject.Parse(CurrentWeatherJson);
			JArray arr = JArray.Parse(obj["weather"]["minutely"].ToString());

			foreach(var item in arr)
			{
				Temperature.Add(0, Convert.ToDouble(item["temperature"]["tc"].ToString()));

				Humidiy.Add(0, Convert.ToDouble(item["humidity"].ToString()));

				string skycode = item["sky"]["code"].ToString();
				SkyCode.Add(0, Enum.IsDefined(typeof(SkyType), skycode) 
					? (SkyType)Enum.Parse(typeof(SkyType), skycode) : SkyType.NOTHING);
			}
		}

		private void ForecastWeatherJsonFarse(string ForecastJson)
		{
			JObject obj = JObject.Parse(ForecastJson);
			JArray arr = JArray.Parse(obj["weather"]["forecast3days"]["fcst3hour"].ToString());

			List<int> releaseTime = new List<int> { 4, 7, 10, 13, 16, 19, 22 };
			foreach(var sky in arr)
			{
				JArray arr2 = JArray.Parse(sky["sky"].ToString());
				
			}
		}
	}
}