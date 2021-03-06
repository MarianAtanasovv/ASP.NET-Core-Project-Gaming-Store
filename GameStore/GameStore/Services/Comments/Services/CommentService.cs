using GameStore.Data.Models;
using GameStore.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly GameShopDbContext data;

        public CommentService(GameShopDbContext data)
        {
            this.data = data;
        }


        public int Add(
            int id,
            string content,
            string username,
            int rating,
            string createdOn,
            int articleId,
            string userId)
        {
            var comment = new Comment
            {
                Id = id,
                Content = content,
                Username = username,
                Rating = rating,
                CreatedOn = DateTime.UtcNow.ToString("r"),
                ArticleId = articleId,
                UserId = userId
                
            };

            this.data.Comments.Add(comment);
            this.data.SaveChanges();

            return comment.Id;

        }

        public CommentQueryServiceModel All(
            int currentPage, 
            int commentsPerPage, 
            int articleId)
        {
            var commentsQuery = this.data.Comments.AsQueryable().Where(x => x.ArticleId == articleId);

            var totalComments = commentsQuery.Count();

            var comments = GetComments(commentsQuery
                .Skip((currentPage - 1) * commentsPerPage)
                .Take(commentsPerPage));

            return new CommentQueryServiceModel
            {
                TotalComments = totalComments,
                Comments = comments,
                CommentsPerPage = commentsPerPage,
                CurrentPage = currentPage
            };
        }

        public bool Delete(int id)
        {
            var comment = this.data.Comments.Find(id);

            if (comment == null)
            {
                return false;
            }

            this.data.Remove(comment);
            this.data.SaveChanges();

            return true;

        }

        public static IEnumerable<AllCommentsViewModel> GetComments(IQueryable<Comment> commentQuery)
        {
            return commentQuery
           .Select(x => new AllCommentsViewModel
           {
               Id = x.Id,
               Content = x.Content,
               CreatedOn = x.CreatedOn,
               Username = x.Username,
               Rating = x.Rating
               
           })
           .ToList();
        }

    }
}
