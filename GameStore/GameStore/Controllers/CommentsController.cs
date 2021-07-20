
using GameStore.Data.Models;
using GameStore.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GameStore.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext data;

        public CommentsController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add(int ArticleId) => View(new AddCommentToArticleViewModel()
        {
            Id = ArticleId
        });

        [HttpPost]
        public IActionResult Add(AddCommentToArticleViewModel model)
        {
            var comment = new Comment
            {
                Id = model.Id,
                Username = model.Username,
                Content = model.Content,
                Rating = model.Rating,
                ArticleId = model.ArticleId,
                CreatedOn = DateTime.UtcNow.ToString("r")

            };

            

            this.data.Comments.Add(comment);
            data.SaveChanges();

            return Redirect($"/Comments/All?ArticleId={model.ArticleId}");
        }

        public IActionResult All(int ArticleId)
        {
            
            var comments = this.data.Comments
                .Where(x => x.ArticleId == ArticleId)
                .Select(x => new AllCommentsViewModel
                {
                    Username = x.Username,
                    Content = x.Content,
                    Rating = x.Rating,
                    CreatedOn = x.CreatedOn
                })
                .ToList();

                var ratings = data.Comments.Where(d => d.ArticleId == ArticleId).FirstOrDefault();
                

            return this.View(comments);
        }
    }
}
