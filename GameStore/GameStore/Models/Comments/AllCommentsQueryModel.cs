using GameStore.Services.Comments;
using System.Collections.Generic;

namespace GameStore.Models.Comments
{
   

    public class AllCommentsQueryModel
    {
        public int CommentsPerPage = 8;

        public string Content { get; set; }
        public int CurrentPage { get; init; } = 1;
        public string Username { get; set; }

        public string CreatedOn { get; set; }

        
        public int Rating { get; set; }
        public int TotalComments { get; set; }

       public IEnumerable<CommentServiceModel> Comments { get; set; }
    }
}
