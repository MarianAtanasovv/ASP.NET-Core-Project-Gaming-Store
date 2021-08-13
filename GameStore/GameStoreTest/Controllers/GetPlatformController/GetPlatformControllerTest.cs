using GameStore.Controllers;
using GameStore.Models.Games;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Data
{
    public class GetPlatformControllerTest
    {
        [Fact]
        public void GamePlatformeShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("GamePlatform/GetPlatform?platform=PC")
            .WithUser())
            .To<GamePlatformController>(x => x.GetPlatform(With.Any<AllGamesQueryModel>(), "PC"));
        }

      
    }
}
