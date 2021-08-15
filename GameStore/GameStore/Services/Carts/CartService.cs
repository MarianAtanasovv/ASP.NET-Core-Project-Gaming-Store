using GameStore.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext data;

        public CartService(ApplicationDbContext data)
        {
            this.data = data;
          
        }

        
        public IEnumerable<CartGameViewServiceModel> UsersCart(string userId)
        {
            return this.data.CartItems.Where(x => x.UserId == userId).Select(x => new CartGameViewServiceModel
            { 
                GameId = x.Game.Id,
                GameCoverImage = x.Game.ImageUrl,
                GameName = x.Game.Title,
                Quantity = x.Quantity,
                GamePrice = x.Game.Price,
                TotalPrice = x.Quantity * x.Game.Price

            }).ToList();
        
        }


        public bool AddProductToCart(int id, string userId)
        {
            var user = data.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var product = data.Games
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (product == null)
            {
                return false;
            }

            if (data.CartItems.Any(c => c.GameId == id && c.UserId == userId))
            {
                var cartItem = data.CartItems
                    .Where(c => c.GameId == id && c.UserId == userId)
                    .FirstOrDefault();

                cartItem.Quantity++;

                data.SaveChanges();
            }
            else
            {
                var cartItem = new Cart()
                {
                    UserId = userId,
                    Quantity = 1,
                    GameId = id,
                    
                    
                };

                data.CartItems.Add(cartItem);
                data.SaveChanges();
            }

            return true;
        }
        public bool Remove(int gameId, string userId)
        {
            var cartItem = data.CartItems
               .Where(s => s.GameId == gameId && s.UserId == userId)
               .FirstOrDefault();

            if (cartItem == null)
            {
                return false;
            }

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                data.SaveChanges();
            }
            if(cartItem.Quantity == 1)
            {
                data.CartItems.Remove(cartItem);
                data.SaveChanges();
            }

            return true;
        }

        public bool Add(int gameId, string userId)
        {
            var cartItem = data.CartItems
                .Where(s => s.GameId == gameId && s.UserId == userId)
                .FirstOrDefault();

            if (cartItem == null)
            {
                return false;
            }

            cartItem.Quantity++;
            data.SaveChanges();

            return true;
        }

    }
}
