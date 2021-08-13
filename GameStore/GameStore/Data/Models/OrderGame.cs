using GameStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        public int Quantity { get; set; }
    }
}
