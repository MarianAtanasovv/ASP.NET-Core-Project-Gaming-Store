namespace GameStore.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public int Rating { get; set; }

        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public string CreatedOn { get; set; }

        public string UserId { get; set; }
    }
}
