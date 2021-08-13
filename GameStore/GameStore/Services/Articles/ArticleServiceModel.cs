using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Articles
{
   
    public class ArticleServiceModel
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
