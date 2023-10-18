using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product product);
        bool Exists(Product product);
    }
}
