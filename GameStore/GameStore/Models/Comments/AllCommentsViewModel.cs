using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Comments
{
    public class AllCommentsViewModel
    {

        public string Username { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public string CreatedOn { get; set; }
    }

}
