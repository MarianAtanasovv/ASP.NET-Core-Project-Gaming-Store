using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class RegisterUserFormModel
    {

        public int UserId { get; set; }
        public string Username { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string ConfirmPassword { get; init; }


    }
}
