using GameStore.Infrastructure;
using GameStore.Services.Carts;
using GameStore.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using MlkPwgen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
                return BadRequest();
            }

            this.order.CreateOrder(userId);
            return RedirectToAction("SendEmail", "SendEmails", new { @userId = userId });
        }

        public IActionResult FinishOrder(string userId)
        {
            if (userId != User.Id())
            {
                return BadRequest();
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
