using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Data.Models
{
    using static DataConstants;

    public class BlogArticle
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ArticleMaxLength)]
        public string Article { get; set; }

        [Required]
        public string CreatedOn { get; set; } 

        public IEnumerable<Comment> Comments { get; set; }

        public int Rating { get; set; }

        [Required]
        public string TrailerUrl { get; set; }
    }
}
