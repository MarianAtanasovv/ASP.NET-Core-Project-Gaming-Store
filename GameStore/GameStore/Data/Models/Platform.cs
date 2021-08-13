using GameStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Data.Models
{
  
    public class Platform
    {
        public Platform()
        {
            Games = new List<Game>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Game> Games { get; set; }
    }
}
