using GameStore.Data;
using GameStore.Data.Models;
using System;
using System.Linq;

namespace GameStore.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext data;


        public OrderService(ApplicationDbContext data)
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
                    UserId = userId
                });

                var itemCart = data.CartItems
                    .Where(p => p.UserId == userId && p.GameId == gameId)
                    .FirstOrDefault();

                data.CartItems.Remove(itemCart);
                data.SaveChanges();
            }
        }

    }
}

