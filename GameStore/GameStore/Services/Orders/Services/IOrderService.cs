namespace GameStore.Services.Orders
{
    public interface IOrderService
    {
        public int CreateOrder(string userId);

        public void FinishOrder(string userId);

       

        
    }
}
