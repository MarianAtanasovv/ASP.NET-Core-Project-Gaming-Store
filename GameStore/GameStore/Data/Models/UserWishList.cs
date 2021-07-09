using System.Collections.Generic;

namespace GamingWebAppDb.Models
{
    public class UserWishList
    {
        public UserWishList()
        {
            Games = new List<Game>();
        }
        public Game Game { get; private set; }

        public int GameId { get; private set; }

        public int UserId { get; private set; }

        public User User { get; private set; }

        public IEnumerable<Game> Games { get;  set; }
    }
}
