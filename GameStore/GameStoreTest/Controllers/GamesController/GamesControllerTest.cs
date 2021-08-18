using GameStore.Controllers;
using GameStore.Models.Games;
using MyTested.AspNetCore.Mvc;
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
