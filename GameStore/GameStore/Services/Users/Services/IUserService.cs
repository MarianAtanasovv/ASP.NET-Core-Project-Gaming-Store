using GameStore.Models.Users;
using System.Collections.Generic;


namespace GameStore.Services.Users
{
    public interface IUserService
    {
        string IdUser(string userId);

        public IEnumerable<AccountOrdersListingViewModel> UsersPurchases(string userId);
    }
}
