using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;

namespace Books2023.DataLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new CategoryRepository(_db);
            CoverTypes = new CoverTypeRepository(_db);
            Products=new ProductRepository(_db);
            Companies = new CompanyRepository(_db);
        }

        public ICategoryRepository Categories { get; private set; }

        public ICoverTypeRepository CoverTypes { get; private set; }

        public IProductRepository Products {get;private set;}

        public ICompanyRepository Companies { get; private set; }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
