using GameStore.Data.Models;
using GameStore.Models.Users;
using GameStore.Services.Users;
using GameStoreTest.Data;
using System;
using System.Linq;
using Xunit;

namespace GameStoreTest.Services
{
    public class UserServiceTests
    {
        [Theory]
        [InlineData("5")]
        public void FindUserId(string userId)
        {
            var data = DatabaseMock.Instance;
            var user = new User
            {
                Id = "5",

            };

            data.Users.Add(user);
            data.SaveChanges();

            var userService = new UserService(data);
            var userData = userService.IdUser(userId);

            Assert.Equal(userData, userId);
        }
        [Theory]
        [InlineData("5")]
        public void FindCart(string userId)
        {
            var data = DatabaseMock.Instance;
            var orderGames = data.OrderGames.Where(x => x.UserId == userId).Select(x => new AccountOrdersListingViewModel
            {
                GameId = x.Game.Id,
                GameCoverImage = x.Game.ImageUrl,
                GameName = x.Game.Title,
                Quantity = x.Quantity,
                GamePrice = x.Game.Price,
                TotalPrice = x.Quantity * x.Game.Price,
                OrderDate = x.Order.OrderDate.ToString()

            }).ToList();

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,

            };

            data.Orders.Add(order);
            data.SaveChanges();

            var userService = new UserService(data);
            var userServiceData = userService.UsersPurchases(userId);
            Assert.Equal(order.UserId, userId);
        }

    }
}
