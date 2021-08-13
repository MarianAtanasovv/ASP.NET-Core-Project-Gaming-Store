using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Test
{
    public class CheckOutControllerTests
    {
        [Fact]
        public void CheckOutShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("/CheckOut/Charge")
            .WithUser())
            .To<CheckOutController>(x => x.Charge());
        }

    }
}
