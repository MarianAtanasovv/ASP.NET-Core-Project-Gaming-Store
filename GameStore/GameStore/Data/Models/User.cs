using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameStore.Data.Models
{
   

    public class User : IdentityUser
    {
        public User()
        {
            this.OrderedGames = new List<OrderGame>();
        }

        public string UserId { get; set; }

        public IEnumerable<OrderGame> OrderedGames{ get; set; }

        

    }
}
