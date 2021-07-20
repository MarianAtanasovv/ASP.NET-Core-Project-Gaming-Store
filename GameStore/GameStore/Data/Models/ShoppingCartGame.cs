using System.ComponentModel.DataAnnotations;
using GameStore.Models;


namespace GameStore.Data.Models
{
    public class ShoppingCartGame
    {
        [Key]
        public int Id { get; private set; }

        public ShoppingCart ShoppingCart { get; private set; }

        public int GameId { get; private set; }

        [Required]
        public int Quantity { get; private set; }
    }
}