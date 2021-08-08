using GameStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Orders
{
    public class OrderingService : IOrderingService
    {
        private readonly ApplicationDbContext data;

        public OrderingService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public int CreateOrder(string userId)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
               
            };

            this.data.Orders.Add(order);
            this.data.SaveChanges();

            return order.Id;
            
        }

        public void FinishOrder(string userId)
        {
            var cartGames = this.data.CartItems.Where(x => x.UserId == userId).ToList();

            var id = this.data.Orders.Where(x => x.UserId == userId).OrderBy(x => x.OrderDate).Select(x => x.Id).LastOrDefault();

            foreach (var game in cartGames)
            {
                var gameId = game.GameId;
                var quantity = game.Quantity;

                data.OrderGames.Add(new OrderGame
                {
                    OrderId = id,
                    GameId = gameId,
                    Quantity = quantity,
                });

                var itemCart = data.CartItems
                    .Where(p => p.UserId == userId && p.GameId == gameId)
                    .FirstOrDefault();

                data.CartItems.Remove(itemCart);
                data.SaveChanges();
            }

            
        }



        public int GetOrderId(string userId)
        {
            var order = this.data.Orders.Where(x => x.UserId == userId).Select(x => x.Id).LastOrDefault();

            return order;
        }
    }
}
