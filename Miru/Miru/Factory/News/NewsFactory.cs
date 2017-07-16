﻿using Miru.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Miru.Factory.News
{
    public class NewsFactory
    {
        private readonly string clientId;
        private readonly string clientSecret;

        public NewsFactory(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public List<NewsItem> GetCurrentNewsItem()
        {
            var json = RequestData();
            return JsonParser(json);
        }

        private string RequestData()
        {
            string uri = @"https://openapi.naver.com/v1/search/news.json";

            var dataParams = new StringBuilder();
            dataParams.Append($@"?query=주요뉴스");
            dataParams.Append($@"&display=20");
            dataParams.Append($@"&sort=sim");

            string result = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Add("X-Naver-Client-Id", UserValue.NAVER_ClientId);
                client.DefaultRequestHeaders.Add("X-Naver-Client-Secret", UserValue.NAVER_ClientSecret);

                HttpResponseMessage response = client.GetAsync(dataParams.ToString()).Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }

        private List<NewsItem> JsonParser(string json)
        {
            var items = new List<NewsItem>();

            var obj = JObject.Parse(json);
            var arr = (JArray)obj["items"];

            foreach (var data in arr)
            {
                items.Add(new NewsItem()
                {
                    Title = (string)data["title"],
                    OriginalLink = (string)data["originallink"],
                    Link = (string)data["link"],
                    Description = (string)data["description"],
                    PubDate = DateTime.Parse((string)data["pubDate"])
                });
            }
            return items;
        }
    }
}