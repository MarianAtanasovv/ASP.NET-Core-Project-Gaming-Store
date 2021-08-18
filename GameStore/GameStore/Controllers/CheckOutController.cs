using GameStore.Services.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Linq;

namespace GameStore.Controllers
{
    public class CheckOutController : Controller
    {
       
        private readonly ICartService cart;

        public CheckOutController(ICartService cart)
        {
            this.cart = cart;
        }
        public ActionResult Charge()
        {
           
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Charge(string stripeEmail, string stripeToken, string userId)
        {
            var cart = this.cart.UsersCart(userId).Select(x => x.TotalPrice).FirstOrDefault();

            var myCharge = new Stripe.ChargeCreateOptions();

            myCharge.Amount = 100 *(Convert.ToInt64(cart));
            myCharge.Currency = "USD";
            myCharge.ReceiptEmail = stripeEmail;
            myCharge.Description = "Sample Charge";
            myCharge.Source = stripeToken;
            myCharge.Capture = true;

            var chargeService = new Stripe.ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);

            return RedirectToAction("CreateOrder", "Orders" , new { userId = userId});

        }
    }
}
