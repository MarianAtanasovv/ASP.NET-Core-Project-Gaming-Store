using GameStore.Data.Models;
using GameStore.Services.Games;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.Games
{
   

    public class AllGamesQueryModel 
    {
        public const int GamesPerPage = 6;

        public string Title { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalGames { get; set; }

        public GameSorting Sorting { get; init; }

        public IEnumerable<string> Titles { get; set; }

        public IEnumerable<GameServiceModel> Games { get; set; }


    }
}
