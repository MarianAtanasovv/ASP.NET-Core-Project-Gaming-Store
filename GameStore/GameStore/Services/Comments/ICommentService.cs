using GameStore.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Comments
{
    public interface ICommentService
    {
        bool Edit(int id,
          string content);

        int Delete(int id);

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
        int articleId);

        IEnumerable<string> AllComments();
    }
}
