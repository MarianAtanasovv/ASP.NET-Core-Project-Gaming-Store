using GameStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{

    public class Cart
    {
        [Required]
        public string UserId { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public string GameCoverImage { get; set; }
    }
}
