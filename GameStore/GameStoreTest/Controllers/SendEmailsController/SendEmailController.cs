using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Controllers.NewFolder
{
    public class SendEmailController
    {
        [Fact]
        public void SendEmailShouldReturnViewAndRedirectWhenDone()
        {
            MyMvc.Pipeline()
               .ShouldMap("/SendEmails/SendEmail?userId=5")
               .To<SendEmailsController>(x => x.SendEmail("5"));
            }
              
    }
}
