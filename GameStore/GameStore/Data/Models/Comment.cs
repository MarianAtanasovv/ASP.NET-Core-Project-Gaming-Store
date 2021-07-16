using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public int Rating { get; set; }

        public BlogArticle Blog { get; set; }

        public int BlogId { get; set; }
    }
}
