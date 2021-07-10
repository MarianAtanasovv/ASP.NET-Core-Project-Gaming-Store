using GameStore.Models.Games;
using GamingWebAppDb;
using GamingWebAppDb.Models;
using Microsoft.AspNetCore.Mvc;
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
            };

            this.data.Games.Add(game);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private System.Collections.Generic.IEnumerable<GameGenreViewModel> GetGenre()
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
