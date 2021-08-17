using GameStore.Services.Emails;
using Microsoft.AspNetCore.Mvc;

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