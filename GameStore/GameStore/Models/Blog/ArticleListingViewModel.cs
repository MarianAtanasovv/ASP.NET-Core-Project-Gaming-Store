using GameStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Blog
{
    public class ArticleListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public string ShortDescription { get; set; }
        public string Article { get; set; }

        public string CreatedOn { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int Rating { get; set; }

        public string TrailerUrl { get; set; }

        public IEnumerable<BlogArticle> Articles { get; set; }

    }
}
