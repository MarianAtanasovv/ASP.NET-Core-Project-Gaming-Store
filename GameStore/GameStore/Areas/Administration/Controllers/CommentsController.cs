using GameStore.Infrastructure;
using GameStore.Services.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Administration.Controllers
{
    public class CommentsController : AdministrationController
    {
        private readonly ICommentService comments;

        public CommentsController(ICommentService comments)
        {
            this.comments = comments;
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            this.comments.Delete(id);
            return Redirect("/Comments/All");
        }
    }
}
