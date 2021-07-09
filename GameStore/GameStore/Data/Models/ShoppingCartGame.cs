using System.ComponentModel.DataAnnotations;

namespace GamingWebAppDb.Models
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