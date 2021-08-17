using GameStore.Services.Articles;
using GameStore.Services.Articles.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.Articles
{

    public class AllArticlesQueryModel : IArticleModel
    {
        public const int ArticlesPerPage = 8;

        public string Title { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalArticles { get; set; }

        public ArticleSorting Sorting { get; init; }

        public IEnumerable<string> Titles { get; set; }

        public IEnumerable<ArticleServiceModel> Articles { get; set; }
    }
}
