using GameStore.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Articles
{
    public interface IArticleService
    {
        int Add(
            int id,
            string title,
            string content,
            string imageUrl,
            string trailerUrl,
            string createdOn);

        ArticleQueryServiceModel All(
            string searchTerm,
            ArticleSorting sorting,
            int currentPage,
            int articlesPerPage,
            string title);

        bool Edit(int id,
          string title,
          string content,
          string imageUrl,
          string trailerUrl);

        int Delete(int id);

        ArticleDetailsServiceModel Details(int id);

        IEnumerable<string> AllArticles();


    }
}
