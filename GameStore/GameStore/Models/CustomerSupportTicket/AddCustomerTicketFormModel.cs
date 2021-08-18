using GameStore.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.CustomerSupportTicket
{
    using static DataConstants;
   

    public class AddCustomerTicketFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CustomerSupportTicketContentMaxLength, MinimumLength = CustomerSupportTicketContentMinLength, ErrorMessage = "Ticket content should be between {2} and {1} characters long.")]
        public string Content { get; set; }

        public string UserId { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Required]
        public string CreatedOn { get; set; } = DateTime.UtcNow.ToString();
    }
}
