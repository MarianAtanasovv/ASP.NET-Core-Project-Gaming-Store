using System;

namespace GameStore.Models.CustomerSupportTicket
{
   

    public class AllCustomerSupportTicketAnswersViewModel
    {

        public string Sender { get; set; } = "Admin";

        public string Content { get; set; }

        public string SentOn { get; set; } = DateTime.UtcNow.ToString();
    }
}
