using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GameStoreTest.Data;
using GameStore.Data.Models;
using GameStore.Services.Comments;

namespace GameStoreTest.Services
{
    public class CommentsServiceTests
    {
        [Fact]
        public void AddShouldAddAComment()
        {
            var data = DatabaseMock.Instance;

            var comment = new Comment()
            {
                Id = 1,
                Content = "test12312test21312",
                Username = "testUsername",
                Rating = 5,
                CreatedOn = "5/6/2005 09:34:42 PM",
                ArticleId = 3,
                UserId = "1"
            };

            var commentsService = new CommentService(data);
            var commentsData = commentsService.Add(1, "test12312test21312", "testUsername", 5, "5/6/2005 09:34:42 PM", 3, "1");
            data.SaveChanges();

            Assert.Equal(1, data.Comments.Count());

        }

        [Fact]
        public void DeleteShouldDeleteAComment()
        {
            var data = DatabaseMock.Instance;

            var comment = new Comment()
            {
                Id = 1,
                Content = "test12312test21312",
                Username = "testUsername",
                Rating = 5,
                CreatedOn = "5/6/2005 09:34:42 PM",
                ArticleId = 3,
                UserId = "1"
            };

            data.Comments.Add(comment);
            data.SaveChanges();

            var commentsService = new CommentService(data);
            var commentsData = commentsService.Delete(1);
            data.SaveChanges();

            Assert.True(commentsData);

        }

        [Fact]
        public void AllCommentsShouldReturnAllComments()
        {
            var data = DatabaseMock.Instance;

            var comment = new Comment()
            {
                Id = 1,
                Content = "test12312test21312",
                Username = "testUsername",
                Rating = 5,
                CreatedOn = "5/6/2005 09:34:42 PM",
                ArticleId = 3,
                UserId = "1"
            };

            data.Comments.Add(comment);
            data.SaveChanges();

            var commentsService = new CommentService(data);
            var commentsData = commentsService.All(1, 2, 3);
            data.SaveChanges();

            Assert.Equal(1, commentsData.Comments.Count());

        }
    }
}
