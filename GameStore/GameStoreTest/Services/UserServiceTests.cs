using GameStore.Data.Models;
using GameStore.Services.Users;
using GameStoreTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStoreTest.Services
{
    public class UserServiceTests
    {
        [Theory]
        [InlineData("5")]
        public void FindUserId(string userId)
        {
            var data = DatabaseMock.Instance;
            var user = new User
            {
                Id = "5",

            };

            data.Users.Add(user);
            data.SaveChanges();

            var userService = new UserService(data);
            var userData = userService.IdUser(userId);

            Assert.Equal(userData, userId);
        }
    }
}
