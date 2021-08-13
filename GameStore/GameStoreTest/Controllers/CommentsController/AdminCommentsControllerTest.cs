using GameStore.Areas.Administration.Controllers;
using GameStore.Models.Comments;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
