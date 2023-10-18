using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;
using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(CoverType coverType)
        {
            if (coverType.Id == 0)
            {
                return _db.CoverTypes.Any(c => c.Name == coverType.Name);
            }
            return _db.CoverTypes.Any(c => c.Name == coverType.Name && c.Id != coverType.Id);

        }

        public void Update(CoverType coverType)
        {
            _db.CoverTypes.Update(coverType);
        }
    }
}
