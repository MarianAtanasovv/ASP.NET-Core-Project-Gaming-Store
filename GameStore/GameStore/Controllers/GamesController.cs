using GameStore.Infrastructure;
using GameStore.Models.Games;
using GameStore.Services.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService games;

        public GamesController(IGameService games)
        {
            this.games = games;
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
