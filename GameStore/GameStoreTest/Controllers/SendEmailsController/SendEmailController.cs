using GameStore.Controllers;
using MyTested.AspNetCore.Mvc;
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
