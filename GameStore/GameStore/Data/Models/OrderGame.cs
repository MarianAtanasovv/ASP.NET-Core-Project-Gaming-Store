using GameStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{
   

    public class OrderGame
    {
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [Required]
        public int Quantity { get; set; }

        public User User { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
