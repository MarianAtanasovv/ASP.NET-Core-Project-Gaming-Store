namespace GameStoreTest.Data
{
    public static class Cart
    {
        public static GameStore.Data.Models.Cart CartWithId(string id) => new() { UserId = id };
    }
}
