using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services.Emails
{
    public interface IEmailSenderService
    {
        public void SendKeyAsync(string userId);
    }
}
