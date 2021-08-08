using GameStore.Infrastructure;
using GameStore.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderingService order;

        public OrdersController(IOrderingService order)
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
            return RedirectToAction("FinishOrder", new { @userId = userId });
        }

        public IActionResult FinishOrder(string userId)
        {
            if (userId != User.Id())
            {
                return BadRequest();
            }

            this.order.FinishOrder(userId);
            return Redirect("Index");

        }
    }
}
