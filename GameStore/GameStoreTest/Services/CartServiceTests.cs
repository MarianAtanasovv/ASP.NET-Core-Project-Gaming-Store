using GameStore.Data.Models;
using GameStore.Models;
using GameStore.Services.Carts;
using GameStoreTest.Data;
using System.Linq;
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

        [Theory]
        [InlineData(5, "5")]
        public void AddProductToCart(int id, string userId)
        {
           
            var data = DatabaseMock.Instance;
            var user = new User()
            {
                Id = "5"
            };

            data.Users.Add(user);
            data.SaveChanges();

            var rightUser = data.Users.Where(x => x.Id == userId).FirstOrDefault();

            Assert.NotNull(rightUser);

            var game = new Game()
            {
                Id = 5
            };

            data.Games.Add(game);
            data.SaveChanges();

            var rightGame = data.Games.Where(x => x.Id == id).FirstOrDefault();

            Assert.NotNull(rightGame);

            var cartItem = new GameStore.Data.Models.Cart()
            {
                UserId = userId,
                Quantity = 1,
                GameId = id
            };
            data.CartItems.Add(cartItem);
            data.SaveChanges();

            Assert.Equal(data.CartItems.Count(), 1);

            var rightCartItem = data.CartItems.Where(x => x.GameId == id && x.UserId == userId).FirstOrDefault();

            rightCartItem.Quantity++;

            data.SaveChanges();

            Assert.Equal(2, rightCartItem.Quantity);


            var cartService = new CartService(data);
            var result = cartService.AddProductToCart(id, userId);

            Assert.True(result);
           
        }

        [Theory]
        [InlineData(5, "5")]
        public void UserShouldBeNull(int id, string userId)
        {
            var data = DatabaseMock.Instance;
            var user = new User()
            {
                Id = "4"
            };

            data.Users.Add(user);
            data.SaveChanges();

            var rightUser = data.Users.Where(x => x.Id == userId).FirstOrDefault();

            var cartService = new CartService(data);
            var result = cartService.AddProductToCart(id, userId);

            Assert.False(result);
            Assert.Null(rightUser);
        }

        [Theory]
        [InlineData(4, "5")]
        public void CartGameShouldBeNull(int id, string userId)
        {
            var data = DatabaseMock.Instance;
            var user = new User()
            {
                Id = "5"
            };

            data.Users.Add(user);
            data.SaveChanges();

            var rightGame = data.Games.Where(x => x.Id == id).FirstOrDefault();

            var cartService = new CartService(data);
            var result = cartService.AddProductToCart(id, userId);

            Assert.False(result);
            Assert.Null(rightGame);
        }

       

        [Theory]
        [InlineData(4, "5")]
        public void CartItemAdd(int id, string userId)
        {
            var data = DatabaseMock.Instance;
            var user = new User()
            {
                Id = "5"
            };

            data.Users.Add(user);
            data.SaveChanges();


            var game = new Game()
            {
                Id = 4
            };

            data.Games.Add(game);
            data.SaveChanges();

            var cartItem = new GameStore.Data.Models.Cart()
            {
                UserId = userId,
                Quantity = 1,
                GameId = id
            };
          ;

            var cartService = new CartService(data);
            var result = cartService.AddProductToCart(id, userId);

            Assert.True(result);
            Assert.Equal(1, data.CartItems.Count());
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

        [Theory]
        [InlineData("5")]
        public void AddProductsToCartCheckUserIdTrue(string userId)
        {
            var data = DatabaseMock.Instance;
            var user = new User()
            {
                Id = "5"
            };
            data.Users.Add(user);
            data.SaveChanges();


            var checkUser = data.Users
                .Where(u => u.Id == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

            Assert.Equal(checkUser, user.Id); ;

        }

        [Theory]
        [InlineData("5")]
        public void AddProductsToCartCheckUserIdFalse(string userId)
        {
            var data = DatabaseMock.Instance;
            var user = new User()
            {
                Id = "4"
            };
            data.Users.Add(user);
            data.SaveChanges();


            var checkUser = data.Users
                .Where(u => u.Id == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

            Assert.NotEqual(checkUser, user.Id); ;

        }


        [Theory]
        [InlineData(5)]
        public void AddProductsToCartCheckProductTrue(int id)
        {
            var data = DatabaseMock.Instance;
            var game = new Game()
            {
                Id = 5
            };
            data.Games.Add(game);
            data.SaveChanges();

            var product = data.Games
                .Where(p => p.Id == id)
                .FirstOrDefault();

            Assert.Equal(product.Id, game.Id); ;

        }



        [Fact]
        public void AddProductsToCartCheckForGame()
        {
            var data = DatabaseMock.Instance;
            var game = new Game()
            {
                Id = 5
            };
            data.Games.Add(game);
            data.SaveChanges();


            Assert.NotEqual(0, game.Id);

        }



        private static GameStore.Data.Models.Cart Cart()
        {
            return new GameStore.Data.Models.Cart
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