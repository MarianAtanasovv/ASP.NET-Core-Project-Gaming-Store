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
        private readonly IOrderingService order;
        private readonly ICartService cart;

        public OrdersController(IOrderingService order, ICartService cart)
        {
            this.order = order;
            this.cart = cart;
        }

       
        
        public IActionResult CreateOrder(string userId)
        {
            if (userId != User.Id())
            {
                return BadRequest();
            }

            this.order.CreateOrder(userId);
            return RedirectToAction("FinishOrder", new { @userId = userId });
        }

        public IActionResult FinishOrder(string userId)
        {
            if (userId != User.Id())
            {
                return BadRequest();
            }


            this.order.FinishOrder(userId);
            return RedirectToAction("SendEmail", new { @userId = userId});

        }

       public void SendEmail(string userId)
        {
          
            this.order.SendKeyAsync(userId);
        }
    }
}
