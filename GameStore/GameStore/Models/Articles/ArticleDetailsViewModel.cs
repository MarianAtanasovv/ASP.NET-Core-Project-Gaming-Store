using GameStore.Data.Models;
using GameStore.Services.Articles.Models;
using System.Collections.Generic;


namespace GameStore.Models.Articles
{

    public class ArticleDetailsViewModel : IArticleModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Content { get; set; }

        public string CreatedOn { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int Rating { get; set; }

        public string TrailerUrl { get; set; }

    }
}
