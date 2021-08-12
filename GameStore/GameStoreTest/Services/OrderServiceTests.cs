using GameStore.Data.Models;
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
