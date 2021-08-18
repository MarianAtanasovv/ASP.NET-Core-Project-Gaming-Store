using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Data.Models
{ 

    using static DataConstants;
   

    public class CustomerSupportTicket
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CustomerSupportTicketContentMaxLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string CreatedOn { get; set; } = DateTime.UtcNow.ToString();


    }
}
