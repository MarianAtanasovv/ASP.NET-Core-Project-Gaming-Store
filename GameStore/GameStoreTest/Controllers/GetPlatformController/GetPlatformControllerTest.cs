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
        public void GetPlatformsShouldReturnGamesWithPlatforms()
        {
            MyMvc.Pipeline()
                .ShouldMap("/Games/GetPlatforms/Pc")
                .To<GamePlatformController>(x => x.GetPlatform(new AllGamesQueryModel
                {

                }, "Pc"))
                .Which(controller => controller.WithData())
                .ShouldReturn()
                .View(view => view.WithModelOfType<AllGamesQueryModel>());

        }
    }
}
