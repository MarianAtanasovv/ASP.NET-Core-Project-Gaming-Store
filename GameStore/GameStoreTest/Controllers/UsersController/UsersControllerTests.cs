using GameStore.Controllers;
using GameStore.Models.CustomerSupportTicket;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace GameStoreTest.Test
{
    public class UsersControllerTests
    {
        [Fact]
        public void AccountHistoryShouldMapCorrectly()
        {
                   MyRouting
              .Configuration()
              .ShouldMap(request => request.WithLocation("Users/AccountHistory/5")
              .WithUser())
              .To<UsersController>(c => c.AccountHistory("5"));
        }

        [Fact]
        public void MyTicketsAnswersShouldMapCorrectly()
        {
            
                MyMvc.Pipeline()
                    .ShouldMap("Users/MyTicketsAnswers?userId=5")
                    .To<UsersController>(x => x.MyTicketsAnswers(new AllCustomerSupportTicketAnswersViewModel
                    {

                    }, "5"))
                    .Which(controller => controller.WithoutData())
                    .ShouldReturn()
                    .View(view => view.WithModelOfType<List<AllCustomerSupportTicketAnswersViewModel>>());

         }
        }
    
}
