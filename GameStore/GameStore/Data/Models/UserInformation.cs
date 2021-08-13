using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Data.Models
{
    using static DataConstants;
    
    public class UserInformation
    {
        public UserInformation()
        {
            this.Orders = new List<Order>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public int AddressId { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
