using GameStore.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.Articles
{
    using static DataConstants;

    public class AddArticleFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength, ErrorMessage = "Article title should be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(ArticleMaxLength, MinimumLength = ArticleMinLength, ErrorMessage = "Article should be between {2} and {1} characters long.")]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


        [Required]
        [Display(Name = "Trailer Url")]
        [Url]
        public string TrailerUrl { get; set; }

    }
}
