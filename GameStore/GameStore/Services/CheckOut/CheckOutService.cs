using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameStore.Services.CheckOut
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ApplicationDbContext data;

        public CheckOutService(ApplicationDbContext data)
        {
            this.data = data;
        }

      
    }
}
