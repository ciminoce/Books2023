using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;
using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Product product)
        {
            if (product.Id == 0)
            {
                return _db.Products.Any(p=>p.ISBN==product.ISBN);
            }
            return _db.Products.Any(p=>p.ISBN==product.ISBN 
            && p.Id!=product.Id) ;
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
