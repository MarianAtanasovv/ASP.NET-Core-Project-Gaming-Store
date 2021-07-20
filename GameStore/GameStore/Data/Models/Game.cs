using GameStore.Data;
using GameStore.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    using static DataConstants;

    public class Game
    {
        public Game()
        {
            ShoppingCartGames = new List<ShoppingCartGame>();
            //WishListGames = new List<UserWishList>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        public string Guide { get; set; }
        public int GuideId { get; set; }


        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get;  set; }

        public int GenreId { get; set; }

        [Required]
        public Genre Genre { get;  set; }

        [Required]
        [MaxLength(GameDescriptionsMaxLenght)]
        public string Description { get;  set; }

        [Required]
        [MaxLength(GameRequirementsMaxLenght)]
        public string Requirements { get;  set; }

        [Required]
        public decimal Price { get;  set;  }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Url]
        public string TrailerUrl { get;  set; }

        public IEnumerable<ShoppingCartGame> ShoppingCartGames { get; set; }

        public Platform Platform { get; set; }

        public int PlatformId { get; set; }

        //public IEnumerable<UserWishList> WishListGames { get; set; }

    }
}
