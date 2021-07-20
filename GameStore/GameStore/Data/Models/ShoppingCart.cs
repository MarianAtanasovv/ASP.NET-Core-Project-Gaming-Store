using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Models;



namespace GameStore.Data.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartGames = new List<ShoppingCartGame>();
        }

        [Key]
        public int ShoppingCartId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        public IEnumerable<ShoppingCartGame> ShoppingCartGames { get; private set; }

    }
}