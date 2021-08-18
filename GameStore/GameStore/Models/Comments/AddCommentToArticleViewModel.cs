using GameStore.Data;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.Comments
{
    using static DataConstants;
   

    public class AddCommentToArticleViewModel
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(CommentUsernameMaxLength, MinimumLength = CommentUsernameMinLength, ErrorMessage = "Comment Username should be between {2} and {1} characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = CommentContentMinLength, ErrorMessage = "Comment Content should be between {2} and {1} characters long.")]
        public string Content { get; set; }

        [Required]
        public int Rating { get; set; }

        public int ArticleId { get; set; }

        public string CreatedOn { get; set; }

        public int UserId { get; set; }


    }
}
