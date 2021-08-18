using GameStore.Data.Models;
using System.Collections.Generic;
using System.Linq;

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
