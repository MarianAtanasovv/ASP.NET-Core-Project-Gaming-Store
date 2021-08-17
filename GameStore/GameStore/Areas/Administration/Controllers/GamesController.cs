using GameStore.Infrastructure;
using GameStore.Models.Games;
using GameStore.Services.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IGameService games;

        public GamesController(ApplicationDbContext data, IGameService games)
        {
           this.data = data;
            this.games = games;
        }

        public IActionResult All([FromQuery] AllGamesQueryModel query)
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

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
           
            return View(new AddGameFormModel
            {
                Genres = this.games.AllGenres(),
                Platforms = this.games.AllPlatforms()
            });
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult Add(AddGameFormModel gameModel)
        {

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

            return RedirectToAction("All", "Games", new { area = "" });
        }
       
        [Authorize]
        public IActionResult Delete(int id)
        {
            
            var game = this.games.Delete(id);

            if (game == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
          
            var game = this.games.Details(id);

            if (game == null)
            {
                return NotFound();
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
        [Authorize]
        public IActionResult Edit(int id, EditGameFormModel game)
        {

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


            return RedirectToAction("All", "Games", new { area = "" });

        }
        [Authorize]
        public IActionResult Details(int id, string information)
        {

            var game = this.games.Details(id);

            if (game == null)
            {
                return NotFound();
            }

            if (information != game.GetInformationGame())
            {
                return Unauthorized();
            }

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
    }
}
