using GameStore.Data.Models;
using GameStore.Models.CustomerSupportTicket;
using GameStore.Services.CustomerSupport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CustomerSupportTicketController : Controller
    {
        private readonly ICustomerSupportService support;
        private readonly UserManager<User> userManager;

        public CustomerSupportTicketController(ICustomerSupportService support, UserManager<User> userManager)
        {
            this.support = support;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Send()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Send(AddCustomerTicketFormModel ticket)
        {
            if (!ModelState.IsValid)
            {

                return View(ticket);
            }
            var userId = this.userManager.GetUserId(User);

            this.support.Send(
                ticket.Content,
                userId,
                ticket.CreatedOn,
                ticket.UserEmail);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

      

    }
}
