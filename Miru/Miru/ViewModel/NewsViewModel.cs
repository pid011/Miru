using Miru.Factory.News;
using Miru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miru.ViewModel
{
    public class NewsViewModel : BindableBase
    {
        private NewsFactory newsFactory;

        public List<NewsItem> NewsList => newsList;
        private List<NewsItem> newsList;

        private string title;
        private string description;
        private string pubDate;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string PubDate
        {
            get => pubDate;
            set => SetProperty(ref pubDate, value);
        }

        public NewsViewModel()
        {
            newsFactory = new NewsFactory(UserValue.NAVER_ClientId, UserValue.NAVER_ClientSecret);

            RefreshNewsList();
        }

        public void RefreshNewsList()
        {
            newsList = newsFactory.GetCurrentNewsItem();
        }

        public void SetNewsView(string title, string description, string pubDate)
        {
            Title = title;
            Description = description;
            PubDate = pubDate;
        }
    }
}
