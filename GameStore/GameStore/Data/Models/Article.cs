using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{
    using static DataConstants;

    public class Article
    {
        public Article()
        {
            Comments = new List<Comment>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ArticleMaxLength)]
        public string Content { get; set; }

        [Required]
        public string CreatedOn { get; set; } 

        public IEnumerable<Comment> Comments { get; set; }

        public int Rating { get; set; }

        [Required]
        public string TrailerUrl { get; set; }

       
        

    }
}
