using GameStore.Models.Users;
using System.Collections.Generic;
using System.Linq;


namespace GameStore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;


        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string IdUser(string userId)
        {
            return this.data.Users.Where(x => x.Id == userId).Select(x => x.Id).FirstOrDefault();
        }


        public IEnumerable<AccountOrdersListingViewModel> UsersPurchases(string userId)
        {
            return this.data.OrderGames.Where(x => x.UserId == userId).Select(x => new AccountOrdersListingViewModel
            {
                GameId = x.Game.Id,
                GameCoverImage = x.Game.ImageUrl,
                GameName = x.Game.Title,
                Quantity = x.Quantity,
                GamePrice = x.Game.Price,
                TotalPrice = x.Quantity * x.Game.Price,
                OrderDate = x.Order.OrderDate.ToString()

            }).ToList();

        }
    }
}
