using GameStore.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Comments
{
    public interface ICommentService
    {

        bool Delete(string id);

        CommentQueryServiceModel All(
            int currentPage,
            int commentsPerPage,
            int articleId
        );

        int Add(
        int id,
        string content,
        string username,
        int rating,
        string createdOn,
        int articleId,
        string userId);

        IEnumerable<string> AllComments();

        
    }
}
