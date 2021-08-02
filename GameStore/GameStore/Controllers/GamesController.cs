using GameStore.Data.Models;
using GameStore.Infrastructure;
using GameStore.Models;
using GameStore.Models.Games;
using GameStore.Services.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService games;

        public GamesController(IGameService games)
        {
            this.games = games;
        }

        [Authorize]
        public IActionResult Add()
        {
            //if (!User.IsAdmin())
            //{
            //    return Unauthorized();
            //}

            return View(new AddGameFormModel
            {
                Genres = this.games.AllGenres(),
                Platforms = this.games.AllPlatforms()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddGameFormModel gameModel)
        {
            //add admin check
            //if (!User.IsAdmin())
            //{
            //    return Unauthorized();
            //}

            if (!this.games.GenreExists(gameModel.GenreId))
            {
                this.ModelState.AddModelError(nameof(gameModel.GenreId), "Genre does not exist.");
            }

            if (!this.games.PlatformExists(gameModel.PlatformId))
            {
                this.ModelState.AddModelError(nameof(gameModel.PlatformId), "Platform does not exist.");
            }

            if (!ModelState.IsValid)
            {
                gameModel.Genres = this.games.AllGenres();
                gameModel.Platforms = this.games.AllPlatforms();

                return View(gameModel);
            }

            this.games.Add(
                gameModel.Id,
                gameModel.Title,
                gameModel.Description,
                gameModel.Requirements,
                gameModel.Guide,
                gameModel.Price,
                gameModel.ImageUrl,
                gameModel.TrailerUrl,
                gameModel.GenreId,
                gameModel.PlatformId);

               return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult All([FromQuery]AllGamesQueryModel query)
        {
            var gamesQueryResult = this.games.All(
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllGamesQueryModel.GamesPerPage,
                query.Title);

            var gameTitles = this.games.AllGames();
              
            query.TotalGames = gamesQueryResult.TotalGames;
            query.Games = gamesQueryResult.Games;
            query.Titles = gameTitles;
            

            return this.View(query);
        }

        public IActionResult Details(int id)
        {
            var game = this.games.Details(id);
            // handle error when there are no games!
            return View(new GameDetailsViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Requirements = game.Requirements,
                Guide = game.Guide,
                ImageUrl = game.ImageUrl,
                TrailerUrl = game.TrailerUrl,
                Platform = game.Platform,
                Genre = game.Genre,
                Price = game.Price
            });

        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var game = this.games.Delete(id);

            return Redirect("/Games/All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            //if (!User.IsAdmin())
            //{
            //    return Unauthorized();
            //}

            var game = this.games.Details(id);

            if(game == null)
            {
                return View("~/Views/Errors/404.cshtml");
            }

            return View(new EditGameFormModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Requirements = game.Requirements,
                Guide = game.Guide,
                ImageUrl = game.ImageUrl,
                Price = game.Price,
                TrailerUrl = game.TrailerUrl,
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, EditGameFormModel game)
        {

            //admin role check

            if (!ModelState.IsValid)
            {
                game.Genres = this.games.AllGenres();
                game.Platforms = this.games.AllPlatforms();

                return View(game);
            }


            this.games.Edit(
               id,
               game.Title,
               game.Description,
               game.Requirements,
               game.Guide,
               game.Price,
               game.ImageUrl,
               game.TrailerUrl);


            return RedirectToAction(nameof(All));

        }

    }
}
