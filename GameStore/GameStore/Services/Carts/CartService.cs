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

        // /????
        public IEnumerable<CartGameViewServiceModel> UsersCart(string userId)
        {
            return this.data.CartItems.Where(x => x.UserId == userId).Select(x => new CartGameViewServiceModel
            { 
                GameCoverImage = x.Game.ImageUrl,
                GameName = x.Game.Title,
                Quantity = x.Quantity,
                GamePrice = x.Game.Price

            }).ToList();
        
        }


        public bool AddProductToCart(int Id, string userId)
        {
            var user = data.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var product = data.Games
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            if (product == null)
            {
                return false;
            }

            if (data.CartItems.Any(c => c.GameId == Id && c.UserId == userId))
            {
                var cartItem = data.CartItems
                    .Where(c => c.GameId == Id && c.UserId == userId)
                    .FirstOrDefault();

                cartItem.Quantity++;

                data.SaveChanges();
            }
            else
            {
                var cartItem = new CartItem()
                {
                    UserId = userId,
                    Quantity = 1,
                    GameId = Id
                };

                data.CartItems.Add(cartItem);
                data.SaveChanges();
            }

            return true;
        }


    }
}
