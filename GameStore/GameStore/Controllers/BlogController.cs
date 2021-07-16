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
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext data;

        public BlogController(ApplicationDbContext data)
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

            var article = new BlogArticle
            {
                Id = model.Id,
                Title = model.Title,
                Article = model.Article,
                ImageUrl = model.ImageUrl,
                TrailerUrl = model.TrailerUrl,
                CreatedOn = DateTime.UtcNow.ToString("r")
               
            };

            this.data.Blogs.Add(article);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int Id)
        {
            var details = this.data.Blogs.Where(x => x.Id == Id)
            .Select(x => new ArticleDetailsViewModel
            {
                Id = x.Id,
                Article = x.Article,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                TrailerUrl = x.TrailerUrl
            })
              .FirstOrDefault();



            return View(details);

        }

        public IActionResult All([FromQuery] AllArticlesQueryModel query)
        {
            var articlesQuery = this.data.Blogs.AsQueryable();

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
                    CreatedOn = DateTime.UtcNow.ToString("r"),
                    ShortDescription = x.Article.Substring(0, 200),
                    Rating = x.Rating,
                    ImageUrl = x.ImageUrl,
                    TrailerUrl = x.TrailerUrl

                })
             .ToList();

            var articleTitles = this.data
                .Blogs
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
