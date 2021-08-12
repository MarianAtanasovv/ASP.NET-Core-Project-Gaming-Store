using FluentAssertions;
using GameStore.Controllers;
using GameStore.Models;
using GameStore.Models.Articles;
using GameStore.Models.Games;
using GameStore.Services.Games;
using Moq;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Controllers.ArticlesControllerTest
{
    using static Data.Articles;
    public class ArticlesControllerTest
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

        [Theory]
        [InlineData(1)]
        public void NotFoundDetailsShouldReturnNotFound(int id)
        {

            MyMvc.Pipeline()
                .ShouldMap(request => request.WithLocation("/Articles/Details/2")
                .WithUser()
                .WithMethod(HttpMethod.Get))
                .To<ArticlesController>(x => x.Details(2))
                .Which()
                .ShouldReturn()
                .View("~/Views/Errors/404.cshtml");


        }

        [Theory]
        [InlineData(1)]

        public void FoundDetailsShouldReturnDetailsPage(int id)
        {

            MyController<ArticlesController>
                 .Instance(controller => controller.WithData(ArticleWithId(id)))
                 .Calling(x => x.Details(id))
                 .ShouldReturn()
                 .View(view => view
                     .WithModelOfType<ArticleDetailsViewModel>()
                     .Passing(m => m.Id == id));


        }

    }
}
