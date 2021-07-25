using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Comments
{
    public class CommentServiceModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Content { get; set; }

        public int ArticleId { get; set; }

        public string CreatedOn { get; set; }
    }
}
