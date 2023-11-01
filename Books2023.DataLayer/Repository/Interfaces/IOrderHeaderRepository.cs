using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository.Interfaces
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

		void UpdateStatus(int orderId, string orderStatus, string? paymentStatus = null);
		void UpdateStripePaymentId(int orderId, string seccionId, string paymentIntentId);
	}
}
