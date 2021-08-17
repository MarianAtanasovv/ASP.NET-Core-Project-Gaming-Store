using GameStore.Data.Models;
using GameStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Games
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext data;

        public GameService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public GameQueryServiceModel All(
            string searchTerm,
            GameSorting sorting,
            int currentPage,
            int gamesPerPage,
            string title)
        {
            var gamesQuery = this.data.Games.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                gamesQuery = gamesQuery.Where(x => x.Title == title);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                gamesQuery = gamesQuery.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            gamesQuery = sorting switch
            {
                GameSorting.Title => gamesQuery.OrderBy(t => t.Title),
                GameSorting.Price => gamesQuery.OrderByDescending(p => p.Price),
                _ => gamesQuery.OrderByDescending(x => x.Id)
            };

            var totalGames = gamesQuery.Count();

            var games = GetGames(gamesQuery
                .Skip((currentPage - 1) * gamesPerPage)
                .Take(gamesPerPage));


            return new GameQueryServiceModel
            {
                TotalGames = totalGames,
                Games = games,
                CurrentPage = currentPage,
                GamesPerPage = gamesPerPage
            };

        }

        public GameDetailsServiceModel Details(int id)
        {
            var details = this.data.Games.Where(x => x.Id == id)
            .Select(x => new GameDetailsServiceModel
            {
                Id = x.Id,
                Requirements = x.Requirements,
                Description = x.Description,
                Guide = x.Guide,
                Title = x.Title,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                TrailerUrl = x.TrailerUrl,
                Genre = x.Genre.Name,
                Platform = x.Platform.Name
            })
              .FirstOrDefault();

            return details;
        }

        public int Add(
            int id,
            string title,
            string description,
            string requirements,
            string guide,
            decimal price,
            string imageUrl,
            string trailerUrl,
            int genreId,
            int platformId)
        {
            var game = new Game
            {
                Id = id,
                Title = title,
                Description = description,
                Requirements = requirements,
                Price = price,
                ImageUrl = imageUrl,
                TrailerUrl = trailerUrl,
                GenreId = genreId,
                Guide = guide,
                PlatformId = platformId

            };

            this.data.Games.Add(game);
            this.data.SaveChanges();

            return game.Id;
        }

        public int Delete(int id)
        {
            var game = this.data.Games.Find(id);

            this.data.Remove(game);
            this.data.SaveChanges();

            return game.Id;
        }

        public bool Edit(
            int id,
            string title,
            string description,
            string requirements,
            string guide,
            decimal price,
            string imageUrl,
            string trailerUrl)
        {
            var gameData = this.data.Games.Find(id);

            if (gameData == null)
            {
                return false;
            }

            gameData.Title = title;
            gameData.Description = description;
            gameData.Requirements = requirements;
            gameData.Guide = guide;
            gameData.Price = price;
            gameData.ImageUrl = imageUrl;
            gameData.TrailerUrl = trailerUrl;
           

            this.data.SaveChanges();

            return true;
        }

        private static IEnumerable<GameServiceModel> GetGames(IQueryable<Game> gameQuery)
        {
            return gameQuery
           .Select(g => new GameServiceModel
           {
               Id = g.Id,
               Title = g.Title,
               Description = g.Description,
               Requirements = g.Requirements,
               Price = g.Price,
               Guide = g.Guide,
               Platform = g.Platform.Name,
               ImageUrl = g.ImageUrl,
               TrailerUrl = g.TrailerUrl,
               Genre = g.Genre.Name

           })
           .ToList();
        }

        public IEnumerable<string> AllGames()
        {
            return this.data
                  .Games
                  .Select(c => c.Title)
                  .Distinct()
                  .OrderBy(br => br)
                  .ToList();
        }

        public IEnumerable<GameGenreServiceModel> AllGenres()
        {
            return this.data
                .Genres
                .Select(c => new GameGenreServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public IEnumerable<GamePlatformServiceModel> AllPlatforms()
        {
            return this.data
                .Platforms
                .Select(c => new GamePlatformServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public bool GenreExists(int genreId)
        {
            return this.data
                .Genres
                .Any(c => c.Id == genreId);
        }
        public bool PlatformExists(int platformId)
        {
            return this.data
                .Platforms
                .Any(c => c.Id == platformId);
        }
    }
}
