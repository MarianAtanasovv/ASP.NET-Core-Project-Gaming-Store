using GameStore.Data.Models;
using GameStore.Models;
using GameStore.Services.Games;
using GameStoreTest.Data;
using System.Linq;
using Xunit;

namespace GameStoreTest.Services
{

    public class GameServiceTests
    {
        [Fact]
        public void GameAddShouldAddGameToDatabase()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            
            var game = CreateGame();
            
            var gameService = new GameService(data);

            //Act
            var result = gameService.Add(game.Id, game.Title, game.Description, game.Requirements, game.Guide, game.Price, game.ImageUrl, game.TrailerUrl, game.GenreId, game.PlatformId); ;
            data.SaveChanges();

            //Assert
            Assert.Equal(1, data.Games.Count());

        }

        [Fact]
        public void GameDetailsShouldReturnTheRightDetails()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();
            var gameService = new GameService(data);
            var gameId = game.Id;

            //Act
            var gameData = data.Games.Find(gameId);

            //Assert
            Assert.NotNull(gameData);

        }
        [Fact]
        public void GenreExistsShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();
            var gameService = new GameService(data);

            var result = gameService.GenreExists(1);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void GenreNotExistingShouldReturnFalse()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();
            var gameService = new GameService(data);

            var result = gameService.GenreExists(66);

            //Assert
            Assert.False(result);

        }

        [Fact]
        public void PlatformNotExistingShouldReturnFalse()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();
            var gameService = new GameService(data);

            var result = gameService.PlatformExists(66);

            //Assert
            Assert.False(result);

        }

        [Fact]
        public void PlatformExistingShouldReturnTrue()
        {
            //Arrange

            var data = DatabaseMock.Instance;

            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();
            var gameService = new GameService(data);

            var result = gameService.PlatformExists(1);

            //Assert
            Assert.True(result);

        }
        [Fact]
        public void GameDeleteShouldDeleteTheGame()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            var id = 1;

            data.Games.Add(CreateGame());

            data.SaveChanges();
            var gameService = new GameService(data);

            //Act
            var result = gameService.Delete(id);

            //Assert
            Assert.Equal(0, data.Games.Count());
            
        }

        [Fact]
        public void RightDetailsShouldBeReturned()
        {
            var data = DatabaseMock.Instance;
            var game = CreateGame();
            data.Games.Add(game);
            data.SaveChanges();

            var gameService = new GameService(data);

            var result = gameService.Details(game.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public void IfDetailsNotFoundReturnNull()
        {
            var data = DatabaseMock.Instance;
            var game = CreateGame();
            data.Games.Add(game);
            data.Games.Remove(game);
            data.SaveChanges();

            var gameService = new GameService(data);

            var result = gameService.Details(game.Id);

            Assert.Null(result);
        }


        [Fact]
        public void EditShouldEditTheGame()
        {
            var data = DatabaseMock.Instance;
            var title = "llala";
            var id = 1;
            var description = "sdasdas";
            var requirements = "sdasdas";
            var guide = "sdasdas";
            var price = 20;
            var imageUrl = "sdasdas";
            var trailerUrl = "sdasdas";


            data.Games.Add(CreateGame());

            data.SaveChanges();
            var gameService = new GameService(data);
            var result = gameService.Edit(id, title, description, requirements, guide, price, imageUrl, trailerUrl);
            Assert.True(result);

        }

        [Fact]
        public void EditShouldFindTheGame()
        {
            var data = DatabaseMock.Instance;
          
            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();
            var gameService = new GameService(data);
            var gameId = game.Id;

            var gameData = data.Games.Find(gameId);
            Assert.NotNull(gameData);

        }

        [Fact]
        public void AllGamesShouldReturnAllGames()
        {
            var data = DatabaseMock.Instance;
            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();

            var gameService = new GameService(data);
            var gameData = gameService.AllGames();

            Assert.Equal(data.Games.Count(), gameData.Count());
        }

        [Fact]
        public void AllGamesShouldReturn0IfNotFound()
        {
            var data = DatabaseMock.Instance;
            var game = CreateGame();
            data.Games.Add(game);
            data.Games.Remove(game);

            data.SaveChanges();

            var gameService = new GameService(data);
            var gameData = gameService.AllGames();

            Assert.Equal(0, gameData.Count());
        }

        [Fact]
        public void AllGamesQuery()
        {
            var data = DatabaseMock.Instance;
            var game = CreateGame();
            data.Games.Add(game);
            data.SaveChanges();

            var gameService = new GameService(data);
            var gameData = gameService.All("Title", 0, 1, 5, "TestGame");

            Assert.NotNull(gameData);
        }

        [Fact]
        public void EditShouldReturnNullIfGameNotFound()
        {
            var data = DatabaseMock.Instance;

            var game = CreateGame();
            data.Games.Add(game);

            data.SaveChanges();
            var gameService = new GameService(data);

            var gameData = data.Games.Find(5);
            
            
            Assert.Null(gameData);
        }

        [Fact]
        public void AllGenresShouldBeFound()
        {
            var data = DatabaseMock.Instance;

            data.Genres.Add(new Genre { Id = 1,
                Name = "Shooter" });
            data.SaveChanges();
            var genres = data.Genres;

            
            var gameService = new GameService(data);

            var gameData = gameService.AllGenres();

            Assert.Equal(gameData.Count(), genres.Count());

        }


        [Fact]
        public void AllPlatformsShouldBeFound()
        {
            var data = DatabaseMock.Instance;

            data.Platforms.Add(new Platform
            {
                Id = 1,
                Name = "Pc"
            });
            data.SaveChanges();
            var genres = data.Platforms;


            var gameService = new GameService(data);

            var gameData = gameService.AllPlatforms();

            Assert.Equal(gameData.Count(), genres.Count());

        }

        public static Game CreateGame()
        {
            return new Game
            {
                Id = 1,
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

            };
        }
    }

   
}
