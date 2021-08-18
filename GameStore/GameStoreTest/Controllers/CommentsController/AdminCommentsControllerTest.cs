using GameStore.Areas.Administration.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace GameStoreTest.Controllers
{
    public class AdminCommentsControllerTest
    {
        [Fact]
        public void DeleteShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("/Administration/Comments/Delete?id=1")
            .WithUser())
            .To<CommentsController>(x => x.Delete(1));
        }
    }
}
