namespace GameStore.Services.Carts
{

    public class CartGameViewServiceModel
    {
        public int GameId { get; set; }

        public string GameName { get; set; }

        public decimal GamePrice { get; set; }

        public string GameCoverImage { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
