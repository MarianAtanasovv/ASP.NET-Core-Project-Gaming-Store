using GameStore.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameStoreTest.Data
{
    public static class Comments
    {
        public static IEnumerable<Comment> GetFiveComments()
        {
            return Enumerable.Range(0, 5).Select(x => new Comment
            {

            });
        }

        public static Comment CommentWithId(int id) => new() { Id = id };

       
    }
}
