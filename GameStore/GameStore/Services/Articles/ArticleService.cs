using GameStore.Data.Models;
using GameStore.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Articles
{
    public class ArticleService : IArticleService
    {

        private readonly ApplicationDbContext data;
        public ArticleService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public int Add(int id,
            string title,
            string content,
            string imageUrl,
            string trailerUrl,
            string createdOn)
        {
            var article = new Article
            {
                Id = id,
                Title = title,
                Content = content,
                ImageUrl = imageUrl,
                TrailerUrl = trailerUrl,
                CreatedOn = createdOn
            };

            this.data.Articles.Add(article);
            this.data.SaveChanges();

            return article.Id;
        }

        public ArticleQueryServiceModel All(
            string searchTerm,
            ArticleSorting sorting,
            int currentPage,
            int articlesPerPage,
            string title)
        {
            var articlesQuery = this.data.Articles.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                articlesQuery = articlesQuery.Where(x => x.Title == title);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                articlesQuery = articlesQuery.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            articlesQuery = sorting switch
            {
                ArticleSorting.Title => articlesQuery.OrderBy(t => t.Title),
                ArticleSorting.CreatedOn => articlesQuery.OrderByDescending(p => p.CreatedOn),
                _ => articlesQuery.OrderByDescending(x => x.Id)
            };

            var totalArticles = articlesQuery.Count();

            var articles = GetArticles(articlesQuery
                .Skip((currentPage - 1) * articlesPerPage)
                .Take(articlesPerPage));

            return new ArticleQueryServiceModel
            {
                TotalArticles = totalArticles,
                Articles = articles,
                CurrentPage = currentPage,
                ArtclesPerPage = articlesPerPage
            };

        }

        public int Delete(int id)
        {
            var article = this.data.Articles.Find(id);

            this.data.Remove(article);
            this.data.SaveChanges();

            return article.Id;
        }

        public bool Edit(int id,
            string title,
            string content,
            string imageUrl,
            string trailerUrl)
        {
            var articleData = this.data.Articles.Find(id);

            if (articleData == null)
            {
                return false;
            }

            articleData.Title = title;
            articleData.Content = content;
            articleData.ImageUrl = imageUrl;
            articleData.TrailerUrl = trailerUrl;

            this.data.SaveChanges();

            return true;
        }

        private static IEnumerable<ArticleServiceModel> GetArticles(IQueryable<Article> articleQuery)
        {
            return articleQuery
           .Select(g => new ArticleServiceModel
           {
               Id = g.Id,
               Title = g.Title,
               Content = g.Content,
               ShortDescription = g.Content.Substring(0, 200),
               Rating = g.Rating,
               CreatedOn = g.CreatedOn,
               ImageUrl = g.ImageUrl,
               TrailerUrl = g.TrailerUrl
           })
           .ToList();
        }

        public ArticleDetailsServiceModel Details(int id)
        {
            var details = this.data.Articles.Where(x => x.Id == id)
             .Select(d => new ArticleDetailsServiceModel
             {
                 Id = d.Id,
                 Title = d.Title,
                 Content = d.Content,
                 CreatedOn = d.CreatedOn,
                 ImageUrl = d.ImageUrl,
                 TrailerUrl = d.TrailerUrl
             })
              .FirstOrDefault();

            return details;
        }

        public IEnumerable<string> AllArticles()
        {
            return this.data
                  .Articles
                  .Select(c => c.Title)
                  .Distinct()
                  .OrderBy(br => br)
                  .ToList();
        }
    }
}

