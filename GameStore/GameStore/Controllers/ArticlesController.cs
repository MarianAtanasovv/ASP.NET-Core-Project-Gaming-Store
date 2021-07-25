using GameStore.Data.Models;
using GameStore.Models.Articles;
using GameStore.Services.Articles;
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddArticleFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.articles.Add(
                model.Id,
                model.Title,
                model.Content,
                model.ImageUrl,
                model.TrailerUrl,
                model.CreatedOn.ToString("r"));

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var details = this.articles.Details(id);

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

        public IActionResult Delete(int id)
        {
            var article = this.articles.Delete(id);

            // add some admin-creator like logic !

            return Redirect("Games/All");
        }

        public IActionResult Edit(int id)
        {
            var article = this.articles.Details(id);

                return View(new EditArticleFormModel
                {
                    Title = article.Title,
                    Content = article.Content,
                    ImageUrl = article.ImageUrl,
                    TrailerUrl = article.TrailerUrl,
                });

        }
      

        [HttpPost]
        public IActionResult Edit(int id, EditArticleFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.articles.Edit(
                id,
                model.Title,
                model.Content,
                model.ImageUrl,
                model.TrailerUrl);

            return RedirectToAction(nameof(All));
        }
    }

    
}
