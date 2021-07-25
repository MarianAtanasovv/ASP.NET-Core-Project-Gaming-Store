using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Articles
{
    public class ArticleQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int ArtclesPerPage { get; set; }

        public int TotalArticles { get; set; }

        public IEnumerable<ArticleServiceModel> Articles { get; set; }
    }
}
