using GameStore;
using GameStore.Controllers;
using Microsoft.Extensions.Caching.Memory;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
