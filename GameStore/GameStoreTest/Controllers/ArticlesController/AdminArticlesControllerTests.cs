using GameStore.Controllers;
using GameStore.Data.Models;
using GameStore.Models.Articles;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Test.Controllers
{
    public class AdminArticlesControllerTests
    {
        [Fact]
        public void AddArticleShouldBeForAuthorizedUsersAndReturnView()
        {

            MyController<GameStore.Areas.Administration.Controllers.ArticlesController>
                .Instance(controller => controller.WithUser("Administrator"))
                .Calling(c => c.Add(With.Default<AddArticleFormModel>()))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post));

        }

        [Fact]
        public void AddShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("/Administration/Articles/Add")
            .WithUser("Adminsitrator"))
            .To<GameStore.Areas.Administration.Controllers.ArticlesController>(x => x.Add());
        }

        [Fact]
        public void DeleteShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("/Administration/Articles/Delete/1")
            .WithUser("Adminsitrator"))
            .To<GameStore.Areas.Administration.Controllers.ArticlesController>(x => x.Delete(1));
        }


        [Fact]
        public void PostAddShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
        {
            MyController<GameStore.Areas.Administration.Controllers.ArticlesController>
                      .Instance(controller => controller.WithUser("Administrator"))
                      .Calling(c => c.Add(new AddArticleFormModel()))
                      .ShouldHave()
                      .ActionAttributes(attributes => attributes
                      .RestrictingForAuthorizedRequests()
                      .RestrictingForHttpMethod(HttpMethod.Post))
                      .AndAlso()
                      .ShouldHave()
                      .InvalidModelState()
                      .AndAlso()
                      .ShouldReturn()
                      .View(result => result
                          .WithModelOfType<AddArticleFormModel>());
        }


        [Theory]
        [InlineData(1, "TestTestTestTestTesst",
            "TestTestTestTTestTestTestTestTesstTestTestTestTestTesstTestTestTestTestTes" +
            "stTestTestTestTestTesstestTesstTestTestTestTestTessTestTestTestTestTesstTestTestTest" +
            "TestTesstTestTestTestTestTesstTestTestTestTestTesstt", 
            "https://i.stack.imgur.com/GsDIl.jpg", "https://www.youtube.com/watch?v=_LcT5sLwmiw")]
        public void PostAddShouldReturnRedirectAndSaveTheArticle(
              int id,
                string title,
               string content,
               string imageUrl,
               string trailerUrl
            )
          => MyController<GameStore.Areas.Administration.Controllers.ArticlesController>
              .Instance(x => x.WithUser("Administrator"))

              .Calling(c => c.Add(new AddArticleFormModel
              {
                  Id = id,
                  Title = title,
                  Content = content,
                  ImageUrl = imageUrl,
                  TrailerUrl = trailerUrl
                 


              }))
               .ShouldReturn()
                  .Redirect(result => result
                      .To<ArticlesController>(c => c.All(With.Any<AllArticlesQueryModel>())));



        [Theory]
        [InlineData(5)]
        public void EditArticleShouldBeOnlyForAuthorizedAndReturnView(int id)
        {

            MyController<GameStore.Areas.Administration.Controllers.ArticlesController>
                .Instance(controller => controller.WithUser("Administrator"))
                .Calling(c => c.Edit(id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Get));

        }

        [Fact]
        public void EditAddShouldReturnViewWithTheSameModelWhenModelStateIsInvalid()
        {
            MyController<GameStore.Areas.Administration.Controllers.ArticlesController>
                   .Instance(controller => controller.WithUser("Administrator"))
                   .Calling(c => c.Edit(5, new EditArticleFormModel()))
                   .ShouldHave()
                   .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests()
                   .RestrictingForHttpMethod(HttpMethod.Post))
                   .AndAlso()
                   .ShouldHave()
                   .InvalidModelState()
                   .AndAlso()
                   .ShouldReturn()
                   .View(result => result
                       .WithModelOfType<EditArticleFormModel>());
        }

        [Theory]
        [InlineData(1, "TestTestTestTestTesst",
              "TestTestTestTTestTestTestTestTesstTestTestTestTestTesstTestTestTestTestTes" +
              "stTestTestTestTestTesstestTesstTestTestTestTestTessTestTestTestTestTesstTestTestTest" +
              "TestTesstTestTestTestTestTesstTestTestTestTestTesstt",
              "https://i.stack.imgur.com/GsDIl.jpg", "https://www.youtube.com/watch?v=_LcT5sLwmiw")]
        public void EditShouldEditArticleAndReturnView(
                   int id,
                string title,
               string content,
               string imageUrl,
               string trailerUrl)
        {

            MyController<GameStore.Areas.Administration.Controllers.ArticlesController>
                .Instance(x => x.WithUser("Administrator"))

                .Calling(c => c.Edit(5, new EditArticleFormModel
                {
                    ArticleId = id,
                    Title = title,
                    Content = content,
                    ImageUrl = imageUrl,
                    TrailerUrl = trailerUrl

                }))
                .ShouldReturn()
                .Redirect(result => result
                    .To<HomeController>(c => c.Index()));
        }
    }
}
