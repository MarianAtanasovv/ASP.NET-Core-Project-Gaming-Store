using GamingWebAppDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class GamesController
    {
        private readonly ApplicationDbContext data;

        public GamesController(ApplicationDbContext data)
        {
            this.data = data;
        }


    }
}
