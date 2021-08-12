using GameStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
