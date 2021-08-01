
using GameStore.Data.Models;
using GameStore.Models.Comments;
using GameStore.Services.Comments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GameStore.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService comments;

        public CommentsController(ICommentService comments)
        {
            this.comments = comments;
        }

        public IActionResult Add(int ArticleId) => View(new AddCommentToArticleViewModel()
        {
            Id = ArticleId
        });

        [HttpPost]
        public IActionResult Add(AddCommentToArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.comments.Add(
                model.Id,
                model.Content,
                model.Username,
                model.Rating,
                model.CreatedOn,
                model.ArticleId
                );

            return Redirect($"/Comments/All?articleId={model.ArticleId}");
        }

        public IActionResult All([FromQuery] AllCommentsQueryModel query, int articleId)
        {
            var commentsQuery = this.comments.All(query.CurrentPage,
                query.CommentsPerPage, articleId);

            var comments = commentsQuery.Comments;

            return this.View(comments);
        }

        public IActionResult Delete(int id)
        {
            this.comments.Delete(id);

            // add some admin-creator like logic !
            return Redirect("/Comments/All");
        }

    }
}
