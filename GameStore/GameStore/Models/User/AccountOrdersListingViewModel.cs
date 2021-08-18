using GameStore.Data.Models;
using System.Collections.Generic;

namespace GameStore.Models.Users
{
   

    public class AccountOrdersListingViewModel
    {
        public Game Game { get; set; }

        public IEnumerable<OrderGame> OrderedGames { get; set; }

        public int GameId { get; set; }

        public string GameName { get; set; }

        public decimal GamePrice { get; set; }

        public string GameCoverImage { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrderDate { get; set; }
    }
}
