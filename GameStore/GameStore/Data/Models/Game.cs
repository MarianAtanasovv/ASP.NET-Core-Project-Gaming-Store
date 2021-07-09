using GameStore.Data;
using GameStore.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamingWebAppDb.Models
{
    using static DataConstants;

    public class Game
    {
        public Game()
        {
            ShoppingCartGames = new List<ShoppingCartGame>();

        }

        [Key]
        public int GameId { get; private set; }

        public int GuideId { get; set; }

        [Required]
        public Guide Guide { get; set; }
        
        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; private set; }
        
        [Required]
        public Genre Genre { get; private set; }

        [Required]
        [MaxLength(GameDescriptionsMaxLenght)]
        public string Description { get; private set; }

        [Required]
        [MaxLength(GameRequirementsMaxLenght)]
        public string Requirements { get; private set; }

        [Required]
        public decimal Price { get; private set;  }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Url]
        public string TrailerUrl { get; private set; }

        public IEnumerable<ShoppingCartGame> ShoppingCartGames { get; private set; }

        public IEnumerable<UserWishList> WishListGames { get; set; }

    }
}
