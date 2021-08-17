using GameStore.Infrastructure;
using GameStore.Models.Comments;
using GameStore.Services.Comments;
using GameStore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService comments;
        private readonly IUserService users;


        public CommentsController(ICommentService comments, IUserService users)
        {
            this.comments = comments;
            this.users = users;
        }

        public IActionResult Add(int ArticleId) => View(new AddCommentToArticleViewModel()
        {
            Id = ArticleId
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCommentToArticleViewModel model)
        {
            var userId = this.users.IdUser(this.User.Id());

            if(userId == null)
            {
                return NotFound();
            }

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
                model.ArticleId,
                userId
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


    }
}
