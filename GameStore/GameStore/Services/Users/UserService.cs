using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string IdUser(string userId)
        {
            return this.data.Users.Where(x => x.Id == userId).Select(x => x.Id).FirstOrDefault();
        }
    }
}
