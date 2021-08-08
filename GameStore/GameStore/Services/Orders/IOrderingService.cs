using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GameStore.Services.Orders
{
    public interface IOrderingService
    {
        public int CreateOrder(string userId);

        public void FinishOrder(string userId);

        public int GetOrderId(string userId);

        public void SendKeyAsync(string userId);
    }
}
