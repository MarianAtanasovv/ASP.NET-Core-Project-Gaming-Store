using GameStore.Data.Models;
using GameStore.Models;
using GameStore.Services.Orders;
using GameStoreTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Services
{
    public class OrderServiceTests
    {
        [Theory]
        [InlineData("1")]
        public void CreateOrderShouldWorkCorrectly(string userId)
        {
            var data = DatabaseMock.Instance;

            var orderService = new OrderService(data);
            var orderData = orderService.CreateOrder(userId);
            data.SaveChanges();
            
        }

        [Fact]
        public void CreateOrderShouldAddOrder()
        {
            var data = DatabaseMock.Instance;
            var order = CreateOrder();
            data.Orders.Add(order);
            data.SaveChanges();

            
            data.SaveChanges();

        }


        [Theory]
        [InlineData("3")]
        public void CreateOrderShouldNotAddOrderIfUserIdNotFound(string userId)
        {
            var data = DatabaseMock.Instance;

            var orderService = new OrderService(data);
            var orderData = orderService.CreateOrder(userId);
            data.SaveChanges();

            Assert.NotEqual(orderData.ToString(), userId);

        }

        [Theory]
        [InlineData("5")]

        public void FinishOrderShouldWorkProperly(string userId)
        {
            var data = DatabaseMock.Instance;

            var orderService = new OrderService(data);
            orderService.FinishOrder(userId);

        }

        [Theory]
        [InlineData("5")]

        public void FinishOrderShouldAddOrderedGames(string userId)
        {
            var data = DatabaseMock.Instance;
            data.OrderGames.Add(new OrderGame
            {
                OrderId = 1,
                GameId = 2,
                Quantity = 3,
            });
            data.SaveChanges();

            var orderService = new OrderService(data);
            orderService.FinishOrder(userId);

            Assert.Equal(1, data.OrderGames.Count());

        }

        [Theory]
        [InlineData("5")]
        public void FinishOrderShouldWorkCorrectly(string userId)
        {
            var data = DatabaseMock.Instance;
            var cartItem = new GameStore.Data.Models.Cart()
            {
                GameId = 5,
                GameCoverImage = "someRandomUrl",
                Quantity = 1,
                Game = new Game
                {
                    Id = 5
                },
                UserId = "5"

            };

            data.CartItems.Add(cartItem);
            data.SaveChanges();

            var order = new Order()
            {
                Id = 5,
                UserId = "5"
            };

            data.Orders.Add(order);
            data.SaveChanges();


            var orderService = new OrderService(data);
            orderService.FinishOrder(userId);


            }

        public static Order CreateOrder()
        {
            return new Order
            {
                UserId = "1",
                OrderDate = DateTime.UtcNow,
            };
        }
    }
}
