using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Administator");
        }
           
    }
}
