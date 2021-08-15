using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace GameStoreTest.Controllers.AbousUsController
{
    public class AboutUsControllerTests
    {
        [Fact]
        public void MyCartShouldMapToTheRightView()
        {
            MyRouting.Configuration()
            .ShouldMap(x => x.WithLocation("/AboutUs/Index")
            .WithUser())
            .To<AboutUsController>(x => x.Index());
        }
    }
}
