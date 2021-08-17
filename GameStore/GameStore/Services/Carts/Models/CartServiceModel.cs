using System;

namespace GameStore.Services.Carts
{

    public class CartServiceModel
    {
        public int CartGameId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public int GameId { get; set; }
    }
}
