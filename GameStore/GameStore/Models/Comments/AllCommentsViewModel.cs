namespace GameStore.Models.Comments
{
   

    public class AllCommentsViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Content { get; set; }
        public int ArticleId { get; set; }

        public int Rating { get; set; }

        public string CreatedOn { get; set; }
    }

}
