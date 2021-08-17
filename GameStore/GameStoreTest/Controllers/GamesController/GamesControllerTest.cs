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


    }
    
}
