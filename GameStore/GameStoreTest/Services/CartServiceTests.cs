using GameStore.Data.Models;
using GameStore.Models;
using GameStore.Services.Carts;
using GameStoreTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Services
{
    public class CartServiceTests
    {

        [Theory]
        [InlineData("5")]
        public void FindCart(string userId)
        {
            var data = DatabaseMock.Instance;
            var cartGames = data.CartItems.Where(x => x.UserId == userId).Select(x => new CartGameViewServiceModel 
            {
                GameId = x.Game.Id,
                GameCoverImage = x.Game.ImageUrl,
                GameName = x.Game.Title,
                Quantity = x.Quantity,
                GamePrice = x.Game.Price,
                TotalPrice = x.Quantity * x.Game.Price

            }).ToList();

            var cart = Cart();

            data.CartItems.Add(cart);
            data.SaveChanges();

            var cartService = new CartService(data);
            var cartData = cartService.UsersCart(userId);
            Assert.Equal(cart.UserId, userId);
        }

        [Fact]
        public void IncreaseQuantityOfProduct()
        {
            var data = DatabaseMock.Instance;
            var cartGames = data.CartItems.Where(x => x.UserId == "5");
            var cart = Cart();

            data.CartItems.Add(cart);
            data.SaveChanges();

            var cartService = new CartService(data);
            var cartData = cartService.Add(cart.Game.Id, cart.UserId);
            Assert.True(cartData);

        }

      

        [Fact]
        public void RemoveProductsFromCart()
        {
            var data = DatabaseMock.Instance;
            var cartGames = data.CartItems.Where(x => x.UserId == "5");
            var cart = Cart();

            data.CartItems.Add(cart);

            var cartService = new CartService(data);
            cartService.Add(cart.Game.Id, cart.UserId);
            data.SaveChanges();


            var cartData = cartService.Remove(cart.Game.Id, cart.UserId);
            data.SaveChanges();
            Assert.True(cartData);

        }



        private static CartItem Cart()
        {
            return new CartItem
            {
                GameId = 5,
                Game = new Game
                {
                    Id = 5,
                    Title = "Title",
                    Description = "Description",
                    Requirements = "Requirements",
                    Price = 20,
                    Guide = "Guide",
                    Platform = new Platform
                    {
                        Id = 1,
                        Name = "Pc"
                    },
                    ImageUrl = "ImageUrl",
                    TrailerUrl = "TrailerUrl",
                    Genre = new Genre
                    {
                        Id = 1,
                        Name = "Shooter"
                    }
                },
                GameCoverImage = "lalalal",
                Quantity = 1,
                UserId = "5"


            };
        }
    }
   
}