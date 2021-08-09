using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore;
using GameStore.Models;
using GameStore.Infrastructure;
using GameStore.Models.Games;
using GameStore.Services.Games;

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

        // GET: Administration/Games
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = data.Games.Include(g => g.Genre).Include(g => g.Platform);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Games/Details/5
       

        //GET: Administration/Games/Add
        public IActionResult Add()
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new AddGameFormModel
            {
                Genres = this.games.AllGenres(),
                Platforms = this.games.AllPlatforms()
            });
        }

        
        [HttpPost]
        public IActionResult Add(AddGameFormModel gameModel)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

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

            return RedirectToAction(nameof(Index));
        }

        // GET: Administration/Games/Edit/5


        // POST: Administration/Games/Delete/5
       
        public IActionResult Delete(int id)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var game = this.games.Delete(id);

            if (game == 0)
            {
                return View("~/Views/Errors/404.cshtml");
            }

            return Redirect("/Games/All");
        }

        public IActionResult Edit(int id)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var game = this.games.Details(id);

            if (game == null)
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
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

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


            return RedirectToAction(nameof(Index));

        }

        private bool GameExists(int id)
        {
            return data.Games.Any(e => e.Id == id);
        }
    }
}
