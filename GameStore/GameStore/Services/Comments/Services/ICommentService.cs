namespace GameStore.Services.Comments
{
    public interface ICommentService
    {

        bool Delete(int id);

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

        
    }
}
