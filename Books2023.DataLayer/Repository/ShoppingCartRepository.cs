using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;
using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementQuantity(ShoppingCart cartInDb, int quantity)
        {
            cartInDb.Quantity -= quantity;
            return cartInDb.Quantity;
        }

        public int IncrementQuantity(ShoppingCart cartInDb, int quantity)
        {
            cartInDb.Quantity += quantity;
            return cartInDb.Quantity;

        }
    }
}
