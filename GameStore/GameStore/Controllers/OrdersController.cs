using GameStore.Infrastructure;
using GameStore.Services.Orders;
using Microsoft.AspNetCore.Mvc;


namespace GameStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService order;

        public OrdersController(IOrderService order)
        {
            this.order = order;
            
        }

        public IActionResult CreateOrder(string userId)
        {
            if (userId != User.Id())
            {
                return Unauthorized();
            }

            this.order.CreateOrder(userId);
            return RedirectToAction("SendEmail", "SendEmails", new { @userId = userId });
        }

        public IActionResult FinishOrder(string userId)
        {
            if (userId != User.Id())
            {
                return Unauthorized();
            }

            this.order.FinishOrder(userId);
            return RedirectToAction("ThankYou");

        }

       public IActionResult ThankYou()
       {
            return View();
       }
    }
}
