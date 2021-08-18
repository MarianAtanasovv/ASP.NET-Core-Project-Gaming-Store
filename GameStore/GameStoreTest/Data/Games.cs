using GameStore.Data.Models;
using GameStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Test.Data
{
    public static class Games
    {
        public static IEnumerable<Game> GetFiveGames()
        {
            return Enumerable.Range(0, 5).Select(x => new Game
            {
                
            });
        }

        public static Game GameWithId(int id)
        {
            return new Game()
            {
                Id = id,
                ImageUrl = "lalallalaa",
                Description = "sadsdasdsa",
                Price = 20,
                TrailerUrl = "sadasdasda",
                Title = "sdasdasda",
                Guide = "sdadasda",
                Requirements = "sdasdas",
                PlatformId = 1,
                GenreId = 1,
                Genre = new Genre
                {
                    Id = 1,
                    Name = "Shooter"
                },
                Platform = new Platform
                {
                    Id = 1,
                    Name = "Pc"
                }


            };
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
