using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{

    public class CustomerSupportTicketAnswer
    {
        public int Id { get; set; }

        [Required]
        public string Sender { get; set; } = "Admin";

        [Required]
        public string Content { get; set; }

        public string SentOn { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
