using GameStore.Data.Models;
using GameStore.Models.Games;
using GamingWebAppDb;
using GamingWebAppDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext data;

        public GamesController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
        {
            return View(new AddGameFormModel
            {
                Genres = this.GetGenre()
            });
        }

        [HttpPost]
        public IActionResult Add(AddGameFormModel gameModel)
        {
            if (!this.data.Genres.Any(c => c.Id == gameModel.GenreId))
            {
                this.ModelState.AddModelError(nameof(gameModel.GenreId), "Genre does not exist.");
            }

            if (!ModelState.IsValid)
            {
                gameModel.Genres = this.GetGenre();

                return View(gameModel);
            }

            var game = new Game
            {
                Id = gameModel.Id,
                Title = gameModel.Title,
                Description = gameModel.Description,
                Requirements = gameModel.Requirements,
                Price = gameModel.Price,
                ImageUrl = gameModel.ImageUrl,
                TrailerUrl = gameModel.TrailerUrl,
                GenreId = gameModel.GenreId,
                Guide = gameModel.Guide
                
            };

            this.data.Games.Add(game);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult All([FromQuery]AllGamesQueryModel query)
        {
            var gamesQuery = this.data.Games.AsQueryable();

            if (!string.IsNullOrEmpty(query.Title))
            {
                gamesQuery = gamesQuery.Where(x => x.Title == query.Title);
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                gamesQuery = gamesQuery.Where(x => x.Title.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            gamesQuery = query.Sorting switch
            {
                GameSorting.Title => gamesQuery.OrderBy(t => t.Title),
                GameSorting.Price => gamesQuery.OrderByDescending(p => p.Price),
                _ => gamesQuery.OrderByDescending(x => x.Id)
            };

            var totalGames = gamesQuery.Count();

            var games = gamesQuery
                .Skip((query.CurrentPage - 1) * AllGamesQueryModel.GamesPerPage)
                .Take(AllGamesQueryModel.GamesPerPage)
                .Select(x => new GameListingViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Requirements = x.Requirements,
                    Guide = x.Guide,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Genre = x.Genre.Name,
                })
             .ToList();

            var gameTitles = this.data
                .Games
                .Select(t => t.Title)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

            query.TotalGames = totalGames;
            query.Games = games;
            query.Titles = gameTitles;

            return this.View(query);
        }

        public IActionResult Details(int Id)
        {
           
            var details = this.data.Games.Where(x => x.Id == Id)
            .Select(x => new GameDetailsViewModel
             {
                 Id = x.Id,
                 Requirements = x.Requirements,
                 Description = x.Description,
                 Guide = x.Guide,
                 Title = x.Title,
                 Price = x.Price,
                 ImageUrl = x.ImageUrl,
                 TrailerUrl = x.TrailerUrl,
                 Genre = x.Genre.Name
             })
              .FirstOrDefault();

            

            return View(details);
        }

        private IEnumerable<GameGenreViewModel> GetGenre()
        {
            var genres = this.data.Genres.Select(x => new GameGenreViewModel
            {
                GenreId = x.Id,
                Name = x.Name
            })
            .ToList();

            return genres;
        }
    }
}
