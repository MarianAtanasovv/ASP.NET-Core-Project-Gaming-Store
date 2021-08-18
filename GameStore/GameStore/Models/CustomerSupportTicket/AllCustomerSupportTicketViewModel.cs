using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.CustomerSupportTicket
{
   

    public class AllCustomerSupportTicketViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserEmail { get; set; }

        public string CreatedOn { get; set; } 
    }
}
