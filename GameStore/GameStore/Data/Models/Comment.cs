using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{
   

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public int Rating { get; set; }

        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public string CreatedOn { get; set; }

        public string UserId { get; set; }

    }
}
