using GameStore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameStoreTest.Data
{
    public static class DatabaseMock
    {
        public static GameShopDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<GameShopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new GameShopDbContext(dbContextOptions);
            }
        }
    }
}
