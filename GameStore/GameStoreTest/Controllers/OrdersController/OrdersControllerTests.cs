using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace GameStoreTest.Test
{
    public class OrdersControllerTests
    {
        [Fact]
        public void CreateOrderShouldMapToTheRightRoute()
        {
            MyRouting
       .Configuration()
       .ShouldMap(request => request.WithLocation("/Orders/CreateOrder?userId=1")
       .WithUser())
       .To<OrdersController>(c => c.CreateOrder("1"));
        
        }

        [Fact]
        public void FinishOrderShouldReturnCorrectView()
        {
            MyRouting
     .Configuration()
     .ShouldMap(request => request.WithLocation("/Orders/FinishOrder?userId=1")
     .WithUser())
     .To<OrdersController>(c => c.FinishOrder("1"));


        }

        [Fact]
        public void TankYouShouldReturnCorrectView()
        {
            MyRouting
     .Configuration()
     .ShouldMap(request => request.WithLocation("/Orders/ThankYou")
     .WithUser())
     .To<OrdersController>(c => c.ThankYou());


        }
    }
}
