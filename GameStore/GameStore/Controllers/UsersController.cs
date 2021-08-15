using GameStore.Infrastructure;
using GameStore.Services.Users;
using Microsoft.AspNetCore.Mvc;


namespace GameStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService user;

        public UsersController(IUserService user)
        {
            this.user = user;
        }


        [Route("Users/AccountHistory/{userId}")]
        public IActionResult AccountHistory(string userId)
        {
            if (this.User.Id() != userId || User.IsAdmin())
            {
                return Unauthorized();
            }

            var usersProducts = user.UsersPurchases(userId);

            return View(usersProducts);
        }
    }
}
