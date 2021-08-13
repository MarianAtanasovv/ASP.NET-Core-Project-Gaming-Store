using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

