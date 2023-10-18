using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface ICoverTypeRepository:IRepository<CoverType>
    {
        void Update(CoverType coverType);
        bool Exists(CoverType coverType);

    }
}
