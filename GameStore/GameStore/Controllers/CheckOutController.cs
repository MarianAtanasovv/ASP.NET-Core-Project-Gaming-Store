using GameStore.Services.Carts;
using GameStore.Services.CheckOut;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ICheckOutService checkOut;
        private readonly ICartService cart;

        

        public CheckOutController(ICheckOutService checkOut, ICartService cart)
        {
            this.checkOut = checkOut;
            this.cart = cart;
        }
        public ActionResult Charge()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Charge(string stripeEmail, string stripeToken, string userId)
        {
            var cart = this.cart.UsersCart(userId).Select(x => x.TotalPrice).FirstOrDefault();
            
            StripeConfiguration.SetApiKey("pk_test_51JLsO5KlfxRzROBJpb5y6e7A66iOj5wfuOUnJFDyTwB0Q9ewEYtacBxOIGgrdvXxrjm8yeQ34ofTqWXGUSyFYGY200zvrdJGSk");
            StripeConfiguration.ApiKey = "sk_test_51JLsO5KlfxRzROBJqb1qNfL01OSeNBxkrc0V2IRobIwPucWbH8uVVjYQZPZ0bXu5mUGlZLnTjOLwzZuAiY6tzgDG00z9xtu2UJ";

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
