using System.Collections.Generic;

namespace GameStore.Services.Games
{

    public class GameQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int GamesPerPage { get; set; }

        public int TotalGames { get; set; }

        public IEnumerable<GameServiceModel> Games { get; set; }
    }
}
