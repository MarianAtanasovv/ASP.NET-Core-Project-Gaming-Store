using GameStore.Controllers;
using GameStore.Data.Models;
using GameStore.Models;
using GameStore.Services.Carts;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Controllers.GamesController
{
    using static Data.Cart;
    public class CartControllerTest
    {
        [Fact]
        public void MyCartShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("/Cart/MyCart/1")
            .WithUser())
            .To<CartController>(x => x.MyCart("1"));
        }
      

    }
}
