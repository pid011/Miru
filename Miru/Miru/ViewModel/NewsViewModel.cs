using Miru.Factory.News;
using Miru.Pages;
using Miru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Miru.ViewModel
{
    public class NewsViewModel : BindableBase
    {
        private NewsFactory newsFactory;

        //private ThreadPoolTimer newsChangeTimer;
        private DispatcherTimer timer;
        private int newsCounter = 1;

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
            SetNewsView(newsList[0].Title, newsList[0].Description, newsList[0].PubDate.ToString());

            timer = new DispatcherTimer();
            timer.Tick += (sender, e) =>
            {
                SetNewsView(
                         newsList[newsCounter].Title,
                         newsList[newsCounter].Description,
                         newsList[newsCounter].PubDate.ToString());

                if (newsCounter == newsList.Count - 1)
                {
                    newsCounter = 0;
                }
                else
                {
                    newsCounter++;
                }
            };
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Start();
        }

        private void RefreshNewsList()
        {
            newsList = newsFactory.GetCurrentNewsItem();
        }

        private void SetNewsView(string title, string description, string pubDate)
        {
            Title = title;
            Description = description;
            PubDate = pubDate;
        }
    }
}
