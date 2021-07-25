using GameStore.Services.Articles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Articles
{
    public class AllArticlesQueryModel
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
