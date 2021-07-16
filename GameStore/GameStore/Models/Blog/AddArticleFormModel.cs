using GameStore.Data;
using GameStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Blog
{
    using static DataConstants;

    public class AddArticleFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength, ErrorMessage = "Article title should be between {1} and {2} characters long.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(ArticleMaxLength, MinimumLength = ArticleMinLength, ErrorMessage = "Article should be between {1} and {2} characters long.")]
        public string Article { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public IEnumerable<Comment> Comments { get; set; }

        [Required]
        [Display(Name = "Trailer Url")]
        [Url]
        public string TrailerUrl { get; set; }
    }
}
