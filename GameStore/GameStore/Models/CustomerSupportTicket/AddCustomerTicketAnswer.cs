using GameStore.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.CustomerSupportTicket
{
    using static DataConstants;
   

    public class AddCustomerTicketAnswer
    {
        public int Id { get; set; }

        [Required]
        public string Sender { get; set; } = "Admin";

        [Required]
        [StringLength(CustomerSupportTicketContentMaxLength, MinimumLength = CustomerSupportTicketContentMinLength, ErrorMessage = "Ticket content should be between {2} and {1} characters long.")]
        public string Content { get; set; }

        public string UserId { get; set; }

        public string SentOn { get; set; } = DateTime.UtcNow.ToString();
    }
}
