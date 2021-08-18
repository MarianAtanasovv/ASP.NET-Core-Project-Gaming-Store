using GameStore.Models.CustomerSupportTicket;
using GameStore.Services.CustomerSupport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameStore.Areas.Administration.Controllers
{
    public class CustomerSupportTicketController : AdministrationController
    {
        private readonly ICustomerSupportService support;

        public CustomerSupportTicketController(ICustomerSupportService support)
        {
            this.support = support;
        }

        public IActionResult All(AllCustomerSupportTicketViewModel model)
        {
            var tickets = this.support.All().Select(x => new AllCustomerSupportTicketViewModel
            {
                Id = x.Id,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                UserEmail = x.UserEmail
            })
                 .ToList();

            return View(tickets);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Answer()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Answer(AddCustomerTicketAnswer ticket)
        {
            if (!ModelState.IsValid)
            {

                return View(ticket);
            }


            this.support.Answer(
                ticket.Content,
                ticket.SentOn,
                ticket.Id);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
