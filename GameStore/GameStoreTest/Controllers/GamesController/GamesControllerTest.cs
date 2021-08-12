using FluentAssertions;
using GameStore.Controllers;
using GameStore.Models;
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

namespace GameStore.Test.Controllers
{
    using static Data.Games;
    public class GamesControllerTest
    {
       [Fact]
       public void AllShouldReturnViewWithAllGames()
        {
            MyMvc.Pipeline()
                .ShouldMap("Games/All")
                .To<GamesController>(x => x.All(new AllGamesQueryModel
                {

                }))
                .Which(controller => controller.WithData(GetFiveGames())
                .ShouldReturn()
                .View(view => view.WithModelOfType<AllGamesQueryModel>()));
                 
        }

        [Theory]
        [InlineData(1)]
        public void NotFoundDetailsShouldReturnNotFound(int id)
        {

            MyMvc.Pipeline()
                .ShouldMap(request => request.WithLocation("/Games/Details/2")
                .WithUser()
                .WithMethod(HttpMethod.Get))
                .To<GamesController>(x => x.Details(2))
                .Which()
                .ShouldReturn()
                .View("~/Views/Errors/404.cshtml");


        }

        [Theory]
        [InlineData(3)]

        public void FoundDetailsShouldReturnDetailsPage(int id)
        {

           MyController<GamesController>
                .Instance(controller => controller.WithData(GameWithId(id)))
                .Calling(x => x.Details(id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GameDetailsViewModel>()
                    .Passing(m => m.Id == id));


        }



    }
    
}
