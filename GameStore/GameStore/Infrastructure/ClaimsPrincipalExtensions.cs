using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
   
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
          
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Administrator");
        }
           
    }
}
