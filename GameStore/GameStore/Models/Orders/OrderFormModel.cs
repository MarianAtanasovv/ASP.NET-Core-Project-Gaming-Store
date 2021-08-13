using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Orders
{
   
    public class OrderFormModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        public string UserId { get; set; }
    }
}
