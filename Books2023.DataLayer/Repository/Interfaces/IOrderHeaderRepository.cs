using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

    }
}
