using GameStore.Areas.Administration;
using System.Security.Claims;

namespace GameStore.Infrastructure
{
    using static AdminConstants;
   

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
          
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdministratorRoleName);
        }
           
    }
}
