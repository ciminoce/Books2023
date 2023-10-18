using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface ICompanyRepository:IRepository<Company>
    {
        void Update(Company company);
        bool Exists(Company company);
    }
}
