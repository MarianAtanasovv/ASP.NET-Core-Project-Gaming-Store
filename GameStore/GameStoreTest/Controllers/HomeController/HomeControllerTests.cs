using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace GameStoreTest.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnTheRightView()
        {
            MyMvc.Pipeline()
                .ShouldMap("/")
                .To<HomeController>(x => x.Index());
                

        }

        [Fact]
        public void ErrorRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Home/Error")
              .To<HomeController>(c => c.Error());
    }
}

