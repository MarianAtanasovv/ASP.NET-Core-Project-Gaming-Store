using GameStore.Models.Comments;
using System.Collections.Generic;

namespace GameStore.Services.Comments
{

    public class CommentQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int CommentsPerPage { get; set; }

        public int TotalComments { get; set; }

        public IEnumerable<AllCommentsViewModel> Comments { get; set; }
    }
}
