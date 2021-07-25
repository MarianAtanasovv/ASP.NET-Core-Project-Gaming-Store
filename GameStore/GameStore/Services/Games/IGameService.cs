using GameStore.Data.Models;
using GameStore.Services.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Games
{
    public interface IGameService
    {
        GameQueryServiceModel All(
            string searchTerm,
            GameSorting sorting,
            int currentPage,
            int gamesPerPage,
            string title);

        GameDetailsServiceModel Details(int id);

        int Add(
            int id, 
            string title,
            string description,
            string requirements,
            string guide, 
            decimal price, 
            string imageUrl,
            string trailerUrl,
            int genreId,
            int platformId);

        bool Edit(int id,
           string title,
           string description,
           string requirements,
           string guide,
           decimal price,
           string imageUrl,
           string trailerUrl);
        int Delete(int id);

        IEnumerable<string> AllGames();

        IEnumerable<GameGenreServiceModel> AllGenres();

        IEnumerable<GamePlatformServiceModel> AllPlatforms();

        bool GenreExists(int genreId);

        bool PlatformExists(int platformId);
    }
}
