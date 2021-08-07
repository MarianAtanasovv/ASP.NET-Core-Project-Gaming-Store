using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        //[Required]
        //public UserInformation User { get; set; }

        public string UserId { get; set; }

        public IEnumerable<OrderGame> OrderGames { get; set; }
    }
}
