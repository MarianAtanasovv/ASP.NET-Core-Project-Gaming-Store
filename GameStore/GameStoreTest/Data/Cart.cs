using GameStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreTest.Data
{
    public static class Cart
    {
        public static CartItem CartWithId(string id) => new() { UserId = id };
    }
}
