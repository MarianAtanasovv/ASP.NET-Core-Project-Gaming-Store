using GameStore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Articles
{
    using static DataConstants;

    public class EditArticleFormModel
    {
        
        public int ArticleId { get; set; }

        [Required]
        [StringLength(ArticleTitleMaxLength, MinimumLength = ArticleTitleMinLength, ErrorMessage = "Article title should be between {1} and {2} characters long.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(ArticleMaxLength, MinimumLength = ArticleMinLength, ErrorMessage = "Article should be between {1} and {2} characters long.")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Trailer Url")]
        [Url]
        public string TrailerUrl { get; set; }

        public string CreatedOn { get; set; }
    }
}
