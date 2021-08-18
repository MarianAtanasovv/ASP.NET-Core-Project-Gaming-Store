using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace GameStoreTest.Controllers.GamesController
{
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
