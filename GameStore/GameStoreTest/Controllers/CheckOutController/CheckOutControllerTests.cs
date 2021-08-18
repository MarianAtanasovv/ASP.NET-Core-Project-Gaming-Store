using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
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
