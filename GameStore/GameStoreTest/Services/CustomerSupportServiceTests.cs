using GameStore.Data.Models;
using GameStore.Services.CustomerSupport;
using GameStoreTest.Data;
using System.Linq;
using Xunit;

namespace GameStoreTest.Services
{
    public class CustomerSupportServiceTests
    {
        [Fact]
        public void SendShouldWorkCorrectly()
        {
            var data = DatabaseMock.Instance;

            var ticket = new CustomerSupportTicket()
            {
                Id = 1,
                Content = "sdadsadasdasdasdasdasdasdasdasda",
                CreatedOn = "5/6/2005 09:34:42 PM",
                UserEmail = "fujin_rajin@abv.bg",
                UserId = "5"
            };
            var user = new User
            {
                Id = "5",
                Email = "fujin_rajin@abv.bg",

            };

            data.Users.Add(user);
            data.SaveChanges();

            data.CustomerSupportTickets.Add(ticket);
            data.SaveChanges();

            var customerService = new CustomerSupportService(data);
            var customerServiceData = customerService.Send(
                "sdadsadasdasdasdasdasdasdasdasda", "" +
                "5", 
                "5/6/2005 09:34:42 PM", 
                "fujin_rajin@abv.bg");

            Assert.NotEqual(customerServiceData, 0);
            Assert.Equal(data.CustomerSupportTickets.Count(), 2);



        }

        [Fact]
        public void AnswerShouldWorkCorrectly()
        {
            var data = DatabaseMock.Instance;

            var ticketSend = new CustomerSupportTicket()
            {
                Id = 1,
                Content = "sdadsadasdasdasdasdasdasdasdasda",
                CreatedOn = "5/6/2005 09:34:42 PM",
                UserEmail = "fujin_rajin@abv.bg",
              
                UserId = "5"
            };
            var ticket = new CustomerSupportTicketAnswer()
            {
                Id = 1,
                Content = "sdadsadasdasdasdasdasdasdasdasda",
                Sender = "Admin", 
                UserId = "5",
                SentOn = "5/6/2005 09:34:42 PM"
            };
            var user = new User
            {
                Id = "5",
                Email = "fujin_rajin@abv.bg",

            };

            data.Users.Add(user);
            data.SaveChanges();


            data.CustomerSupportTickets.Add(ticketSend);
            
            data.CustomerSupportTicketAnswers.Add(ticket);
            data.SaveChanges();
         
            var customerService = new CustomerSupportService(data);
            var customerServiceData = customerService.Answer(
                "sdadsadasdasdasdasdasdasdasdasda",
                "5/6/2005 09:34:42 PM",
                1);

          
            Assert.NotEqual(customerServiceData, 0);
            Assert.Equal(data.CustomerSupportTicketAnswers.Count(), 2);
            Assert.Equal(data.CustomerSupportTickets.Count(), 0);



        }

        [Fact]
        public void AllShouldReturnAllTickets()
        {
            var data = DatabaseMock.Instance;

            var ticketSend = new CustomerSupportTicket()
            {
                Id = 1,
                Content = "sdadsadasdasdasdasdasdasdasdasda",
                CreatedOn = "5/6/2005 09:34:42 PM",
                UserEmail = "fujin_rajin@abv.bg",

                UserId = "5"
            };
            data.CustomerSupportTickets.Add(ticketSend);
            data.SaveChanges();

            var service = new CustomerSupportService(data);
            var serviceData = service.All();

            Assert.Equal(1, serviceData.Count());
        }
    }
}
