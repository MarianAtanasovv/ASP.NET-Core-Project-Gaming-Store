using GameStore.Infrastructure;
using GameStore.Services.Carts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService) => this.cartService = cartService;

        public IActionResult MyCart(string id)
        {
            if (this.User.Id() != id || User.IsAdmin())
            {
                return Unauthorized();
            }

            var usersProducts = cartService.UsersCart(id);

            return View(usersProducts);
        }

        public IActionResult AddToCart(int id, string userId)
        {
            if (this.User.Id() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            cartService.AddProductToCart(id, userId);

            return RedirectToAction("All", "Games");
        }

        public IActionResult Delete(string id) => RedirectToAction("MyCart");
    }
}

