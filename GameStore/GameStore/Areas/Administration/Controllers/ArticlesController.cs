using GameStore.Infrastructure;
using GameStore.Models.Articles;
using GameStore.Services.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Areas.Administration.Controllers
{
    public class ArticlesController : AdministrationController
    {
        private readonly ApplicationDbContext data;
        private readonly IArticleService articles;

        public ArticlesController(ApplicationDbContext data, IArticleService articles)
        {
            this.data = data;
            this.articles = articles;
        }

        public IActionResult Add()
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddArticleFormModel model)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

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

        [Authorize]
        public IActionResult Details(int id)
        {
            var details = this.articles.Details(id);

            if (details == null)
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

        [Authorize]
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

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var article = this.articles.Delete(id);

            if (article == 0)
            {
                return View("~/Views/Errors/404.cshtml");
            }

            return Redirect("Articles/All");
        }

        [Authorize]
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
        [Authorize]
        public IActionResult Edit(int id, EditArticleFormModel model)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

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
