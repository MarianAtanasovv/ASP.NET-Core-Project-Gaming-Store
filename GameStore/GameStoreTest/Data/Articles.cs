using GameStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreTest.Data
{
    public static class Articles
    {
        public static IEnumerable<Article> GetFiveArticles()
        {
            return Enumerable.Range(0, 5).Select(x => new Article
            {

            });
        }

        public static Article ArticleWithId(int id) => new() { Id = id };
    }
}
