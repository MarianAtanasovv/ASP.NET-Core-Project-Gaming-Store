using GameStore.Data.Models;
using GameStore.Models.CustomerSupportTicket;
using System;
using System.Collections.Generic;
using System.Linq;



namespace GameStore.Services.CustomerSupport
{

    public class CustomerSupportService : ICustomerSupportService
    {
        private readonly GameShopDbContext data;

        public CustomerSupportService(GameShopDbContext data)
        {
            this.data = data;
        }

        public int Send(
            string content,
            string userId,
            string createdOn,
            string userEmail)
        {
            var user = this.data.Users.Where(x => x.Id == userId).FirstOrDefault();
            var email = user.Email;

            var ticket = new CustomerSupportTicket
            {
                Content = content,
                UserId = userId,
                CreatedOn = DateTime.UtcNow.ToString(),
                UserEmail = email

            };

                this.data.CustomerSupportTickets.Add(ticket);
                this.data.SaveChanges();

                return ticket.Id;
            }

        public int Answer(
            string content,
            string sentOn, 
            int id)
        {
            var rightUserId = this.data.CustomerSupportTickets.Where(x => x.Id == id).Select(x => x.UserId).FirstOrDefault();

            var answer = new CustomerSupportTicketAnswer
            {
                Content = content,
                Sender = "Admin",
                UserId = rightUserId,
                SentOn = sentOn

            };

            var ticket = this.data.CustomerSupportTickets.Where(x => x.Id == id).FirstOrDefault();

            this.data.CustomerSupportTicketAnswers.Add(answer);
            this.data.CustomerSupportTickets.Remove(ticket);
            this.data.SaveChanges();

            return answer.Id;
        }

        public  IEnumerable<AllCustomerSupportTicketViewModel> All()
        {
            return this.data.CustomerSupportTickets.Select(x => new AllCustomerSupportTicketViewModel
            {
                Id = x.Id,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                UserEmail = x.UserEmail
            })
                 .ToList();

        }

    }
}
