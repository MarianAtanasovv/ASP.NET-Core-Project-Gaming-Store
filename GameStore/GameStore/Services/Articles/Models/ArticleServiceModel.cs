using GameStore.Services.Articles.Models;

namespace GameStore.Services.Articles
{
   

    public class ArticleServiceModel : IArticleModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public string CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string TrailerUrl { get; set; }

        public string ShortDescription { get; set; }

        public int Rating { get; set; }


    }
}
