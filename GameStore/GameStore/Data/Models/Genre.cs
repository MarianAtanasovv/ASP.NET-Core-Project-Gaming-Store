using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamingWebAppDb.Models
{
    public class Genre
    {
        public Genre()
        {
            Games = new List<Game>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Game> Games { get; set; }

    }
}
