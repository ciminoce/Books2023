using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category);
        bool Exists(Category category);
    }
}
