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
		public enum		SkyType
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
			NOTHING
		}

		/// <summary>
		/// 온도
		/// </summary>
		public double	Temperature { get; protected set; }

		/// <summary>
		/// 습도
		/// </summary>
		public double	Humidiy { get; protected set; }

		/// <summary>
		/// 현재 하늘상태
		/// </summary>
		public SkyType	SkyCode { get; protected set; }
		public bool		IsError { get; protected set; } = false;
		public string	ErrorMsg { get; protected set; }
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
			JObject obj1 = JObject.Parse(CurrentWeatherJson);
			JArray arr1 = JArray.Parse(obj1["weather"]["minutely"].ToString());

			foreach(var item in arr1)
			{
				Temperature = Convert.ToDouble(item["temperature"]["tc"].ToString());

				Humidiy = Convert.ToDouble(item["humidity"].ToString());

				string skycode = item["sky"]["code"].ToString();
				SkyCode = Enum.IsDefined(typeof(SkyType), skycode) ? (SkyType)Enum.Parse(typeof(SkyType), skycode) : SkyType.NOTHING;
			}
		}
		private void ForecastWeatherJsonFarse(string ForecastJson)
		{
		}
	}
}