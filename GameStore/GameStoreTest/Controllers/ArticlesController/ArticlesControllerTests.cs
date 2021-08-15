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
    using static Data.Articles;
    public class ArticlesControllerTests
    {
        
   
        [Fact]
        public void AllShouldReturnViewWithAllArticles()
        {
            MyMvc.Pipeline()
                .ShouldMap("Articles/All")
                .To<ArticlesController>(x => x.All(new AllArticlesQueryModel
                {

                }))
                .Which(controller => controller.WithData(GetFiveArticles())
                .ShouldReturn()
                .View(view => view.WithModelOfType<AllArticlesQueryModel>()));

        }

        //[Theory]
        //[InlineData(1)]
        //public void NotFoundDetailsShouldReturnNotFound(int id)
        //{

        //    MyMvc.Pipeline()
        //        .ShouldMap(request => request.WithLocation("/Articles/Details/2")
        //        .WithUser()
        //        .WithMethod(HttpMethod.Get))
        //        .To<ArticlesController>(x => x.Details(2))
        //        .Which()
        //        .ShouldReturn()
        //        .View("~/Views/Errors/404.cshtml");


        //}

        //[Theory]
        //[InlineData(1)]

        //public void FoundDetailsShouldReturnDetailsPage(int id)
        //{

        //    MyController<ArticlesController>
        //         .Instance(controller => controller.WithData(ArticleWithId(id)))
        //         .Calling(x => x.Details(id))
        //         .ShouldReturn()
        //         .View(view => view
        //             .WithModelOfType<ArticleDetailsViewModel>()
        //             .Passing(m => m.Id == id));


        //}
        [Fact]
        public void AllShouldReturnAllArticles()
        {
            MyController<GameStore.Controllers.ArticlesController>
               .Instance()
               .WithData(new Article
               {
                   Id = 1,
                   Title = "TestTestTest",
                   Content = "TestTestTestTTestTestTestTestTesstT" +
                   "estTestTestTestTesstTestTestTestTestTesstTestTestT" +
                   "estTestTesstestTesstTestTestTestTestTessTestTestTestTest" +
                   "TesstTestTestTestTestTesstTestTestTestTestTesstTestTestTestTestTesstt",
                   ImageUrl = "test",
                   TrailerUrl = "test",
                   CreatedOn = DateTime.UtcNow.ToString()
               })
               .Calling(x => x.All(new AllArticlesQueryModel
               {
                   CurrentPage = 1,
                   TotalArticles = 3,
                   SearchTerm = "Title",
                   Sorting = 0,
                   Title = "Test"
               }))
                 .ShouldReturn()
               .View(x => x.WithModelOfType<AllArticlesQueryModel>());


        }



    }
}
