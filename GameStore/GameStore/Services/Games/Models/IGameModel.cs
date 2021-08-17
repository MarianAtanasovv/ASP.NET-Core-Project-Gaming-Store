namespace GameStore.Services.Games.Models
{
    public interface IGameModel
    {
        public string Title { get; }

        public string Platform { get; }

        public string Genre { get; set; }
    }
}
