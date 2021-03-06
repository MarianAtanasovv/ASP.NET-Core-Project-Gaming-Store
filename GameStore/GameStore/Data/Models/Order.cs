using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{
   

    public class Order
    {
        public Order()
        {
            this.OrderGames = new List<OrderGame>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

        public IEnumerable<OrderGame> OrderGames { get; set; }
    }
}
