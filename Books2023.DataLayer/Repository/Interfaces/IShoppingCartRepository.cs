using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int IncrementQuantity(ShoppingCart cartInDb, int quantity);
        int DecrementQuantity(ShoppingCart cartInDb, int quantity);

    }
}
