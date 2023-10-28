using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
