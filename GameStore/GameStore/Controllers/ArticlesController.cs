using GameStore.Data.Models;
using GameStore.Infrastructure;
using GameStore.Models.Articles;
using GameStore.Services.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService articles;

        public ArticlesController(IArticleService article)
        {
            this.articles = article;
        }

       
        public IActionResult Details(int id)
        {
            var details = this.articles.Details(id);

            if(details == null)
            {
                return View("~/Views/Errors/404.cshtml");
            }

            return View(new ArticleDetailsViewModel
            {
                Id = details.Id,
                Content = details.Content,
                Title = details.Title,
                ImageUrl = details.ImageUrl,
                TrailerUrl = details.TrailerUrl,
                CreatedOn = details.CreatedOn,

            });
        }


        public IActionResult All([FromQuery] AllArticlesQueryModel query)
        {
            var articlesQueryResult = this.articles.All(
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllArticlesQueryModel.ArticlesPerPage,
                query.Title);

            var articleTitles = this.articles.AllArticles();

            query.TotalArticles = articlesQueryResult.TotalArticles;
            query.Articles = articlesQueryResult.Articles;
            query.Titles = articleTitles;

            return this.View(query);
        }

       
    }

    
}
