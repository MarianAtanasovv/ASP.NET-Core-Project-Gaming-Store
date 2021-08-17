using GameStore.Data.Models;
using System.Collections.Generic;

namespace GameStore.Models.Articles
{

    public class ArticleListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public string ShortDescription { get; set; }
        public string Content { get; set; }

        public string CreatedOn { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int Rating { get; set; }

        public string TrailerUrl { get; set; }

        public IEnumerable<Article> Articles { get; set; }

    }
}
