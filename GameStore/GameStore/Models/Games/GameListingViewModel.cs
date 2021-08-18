namespace GameStore.Controllers
{
   

    public class GameListingViewModel
    {
        public int Id { get; set; }

        public string Guide { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string TrailerUrl { get; set; }

        public string Platform { get; set; }

    }
}