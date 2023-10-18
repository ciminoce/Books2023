using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;
using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Category category)
        {
            if (category.Id == 0)
            {
                return _db.Categories.Any(c=>c.Name== category.Name);
            }
            return _db.Categories.Any(c=>c.Name==category.Name && c.Id!= category.Id);    
        }


        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
