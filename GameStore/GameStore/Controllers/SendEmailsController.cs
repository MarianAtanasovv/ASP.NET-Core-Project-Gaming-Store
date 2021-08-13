using GameStore.Services.Emails;
using GameStore.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class SendEmailsController : Controller
    {
        private readonly IEmailSenderService email;

        public SendEmailsController(IEmailSenderService email)
        {
            this.email = email;
        }

        public IActionResult SendEmail(string userId)
        {

            this.email.SendKeyAsync(userId);
            return RedirectToAction("FinishOrder", "Orders", new { userId});

        }
    }
}