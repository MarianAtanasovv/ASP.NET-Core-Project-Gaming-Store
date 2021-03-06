using GameStore.Infrastructure;
using GameStore.Services.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService) => this.cartService = cartService;


        [Authorize]
        [HttpGet]
        public IActionResult MyCart(string id)
        {
            if (this.User.Id() != id || User.IsAdmin())
            {
                return Unauthorized();
            }

            var usersProducts = cartService.UsersCart(id);

            return View(usersProducts);
        }

        [Authorize]
        public IActionResult AddToCart(int id, string userId)
        {
            if (this.User.Id() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            cartService.AddProductToCart(id, userId);

            return RedirectToAction("All", "Games");
        }

        [Authorize]
        public IActionResult Remove(int gameId, string userId)
        {
            if (userId != User.Id())
            {
                return Unauthorized();
            }

            cartService.Remove(gameId, userId);

            return Redirect("MyCart/" + userId);
        }

        [Authorize]
        public IActionResult Add(int gameId, string userId)
        {
            if (userId != User.Id())
            {
                return Unauthorized();
            }

            cartService.Add(gameId, userId);

            return Redirect("MyCart/" + userId);
        }
        public IActionResult Delete(string id) => RedirectToAction("MyCart");
    }
}

