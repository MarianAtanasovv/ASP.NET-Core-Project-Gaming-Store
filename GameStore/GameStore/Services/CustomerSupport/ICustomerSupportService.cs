using GameStore.Models.CustomerSupportTicket;
using System.Collections.Generic;

namespace GameStore.Services.CustomerSupport
{
    public interface ICustomerSupportService
    {
        int Send(
            string content, 
            string userId,
            string createdOn,
            string userEmail);

        public int Answer(
           string content,
           string sentOn,
           int id);

        public IEnumerable<AllCustomerSupportTicketViewModel> All();
    }
}
