using System.Collections.Generic;

namespace GameStore.Services.Carts
{
    public interface ICartService
    {
        public IEnumerable<CartGameViewServiceModel> UsersCart(string Id);

        public bool AddProductToCart(int Id,
            string userId);

        public bool Remove(int gameId, 
            string userId);

        public bool Add(int gameId, 
            string userId);

    }
}
