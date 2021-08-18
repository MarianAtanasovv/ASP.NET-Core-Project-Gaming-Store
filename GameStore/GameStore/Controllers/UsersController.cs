using GameStore.Infrastructure;
using GameStore.Models.CustomerSupportTicket;
using GameStore.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService user;

        public UsersController(IUserService user)
        {
            this.user = user;
        }

        [Route("Users/AccountHistory/{userId}")]
        public IActionResult AccountHistory(string userId)
        {
            if (this.User.Id() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            var usersProducts = user.UsersPurchases(userId);

            return View(usersProducts);
        }

        public IActionResult MyTicketsAnswers(AllCustomerSupportTicketAnswersViewModel ticket, string userId)
        {
            var tickets = this.user.MyTicketsAnswers(userId).Select(x => new AllCustomerSupportTicketAnswersViewModel
            {
                SentOn = x.SentOn,
                Content = x.Content,
                Sender = x.Sender
            })
                .ToList();

            return View(tickets);
        }
    }
}
