using GameStore.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamingWebAppDb.Models
{
    using static DataConstants;

    public class User
    {
        public User()
        {
            //WishListGames = new List<UserWishList>();
            //Games = new List<Game>();
        }
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        //public IEnumerable<Game> Games { get; private set; }

        public ShoppingCart ShoppingCart { get; private set; }

        //public IEnumerable<UserWishList> WishListGames { get; set; }


    }
}