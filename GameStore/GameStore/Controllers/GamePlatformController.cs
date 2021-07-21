using GameStore.Data.Models;
using GameStore.Models.Games;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class GamePlatformController : Controller
    {
        private readonly ApplicationDbContext data;

        public GamePlatformController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult GetPlatform([FromQuery] AllGamesQueryModel query, string platform)
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

                var games = gamesQuery.Where(x => x.Platform.Name == platform)
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
                        Platform = x.Platform.Name
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



            return View(query);
        }
    }
}
