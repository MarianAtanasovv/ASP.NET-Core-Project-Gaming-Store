using GameStore.Data.Models;
using GameStore.Models.Blog;
using GamingWebAppDb;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext data;

        public ArticleController(ApplicationDbContext data)
        {
            this.data = data;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(AddArticleFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                ImageUrl = model.ImageUrl,
                TrailerUrl = model.TrailerUrl,
                CreatedOn = DateTime.UtcNow.ToString("r"),
               
            };

            this.data.Articles.Add(article);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int Id)
        {
            var details = this.data.Articles.Where(x => x.Id == Id)
            .Select(x => new ArticleDetailsViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                TrailerUrl = x.TrailerUrl,
                CreatedOn = x.CreatedOn,
                
            })
              .FirstOrDefault();

          

            return View(details);

        }

        public IActionResult All([FromQuery] AllArticlesQueryModel query)
        {
            var articlesQuery = this.data.Articles.AsQueryable();

            if (!string.IsNullOrEmpty(query.Title))
            {
                articlesQuery = articlesQuery.Where(x => x.Title == query.Title);
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                articlesQuery = articlesQuery.Where(x => x.Title.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            articlesQuery = query.Sorting switch
            {
                ArticleSorting.Title => articlesQuery.OrderBy(t => t.Title),
                ArticleSorting.CreatedOn => articlesQuery.OrderByDescending(p => p.CreatedOn),
                _ => articlesQuery.OrderByDescending(x => x.Id)
            };

            var totalArticles = articlesQuery.Count();

            var articles = articlesQuery
                .Skip((query.CurrentPage - 1) * AllArticlesQueryModel.ArticlesPerPage)
                .Take(AllArticlesQueryModel.ArticlesPerPage)
                .Select(x => new ArticleListingViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedOn = x.CreatedOn,
                    ShortDescription = x.Content.Substring(0, 200),
                    Rating = x.Rating,
                    ImageUrl = x.ImageUrl,
                    TrailerUrl = x.TrailerUrl,

                })
             .ToList();

            var articleTitles = this.data
                .Articles
                .Select(t => t.Title)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

            query.TotalArticles = totalArticles;
            query.Articles = articles;
            query.Titles = articleTitles;

            return this.View(query);
        }
    }

    
}
