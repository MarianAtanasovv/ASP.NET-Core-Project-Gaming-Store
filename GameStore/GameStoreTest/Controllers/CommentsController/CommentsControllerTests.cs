using GameStore.Controllers;
using GameStore.Models.Comments;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace GameStoreTest.Test.Controllers
{
    using static Data.Comments;

    public class CommentsControllerTests
    {
        [Theory]
        [InlineData(3)]
        public void AllShouldReturnViewWithAllComments(int id)
        {
            MyController<CommentsController>
                  .Instance(controller => controller.WithData(CommentWithId(id)))
                  .Calling(x => x.All(new AllCommentsQueryModel { }, id))
                  .ShouldReturn()
                  .View(view => view
                      .WithModelOfType<List<AllCommentsViewModel>>());

        }

      
    }
}
