using GameStore.Controllers;
using GameStore.Data.Models;
using GameStore.Models.Articles;
using MyTested.AspNetCore.Mvc;
using System;
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
