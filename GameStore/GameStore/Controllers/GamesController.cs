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

            var games = gamesQuery
                .Select(x => new AllGamesViewModel
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

          
            return this.View(new AllGamesQueryModel 
            { 
                Games = games,
                SearchTerm = query.SearchTerm
            });
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
