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

        public NewsViewModel()
        {
            newsFactory = new NewsFactory(UserValue.NAVER_ClientId, UserValue.NAVER_ClientSecret);

            RefreshNewsList();
        }

        public void RefreshNewsList()
        {
            newsList = newsFactory.GetCurrentNewsItem();
        }
    }
}
