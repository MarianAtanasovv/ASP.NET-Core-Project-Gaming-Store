using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Data.Models
{
   
    public class User : IdentityUser
    {
        public string UserId { get; set; }

        

    }
}
