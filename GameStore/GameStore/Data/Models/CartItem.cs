using GameStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Data.Models
{
    public class CartItem
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
