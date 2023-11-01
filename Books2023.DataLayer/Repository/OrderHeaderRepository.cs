using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;
using Books2023.Models.Models;

namespace Books2023.DataLayer.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader);
        }

		public void UpdateStatus(int orderId, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb=_db.OrderHeaders.FirstOrDefault(o=>o.Id==orderId); ;
			if (orderFromDb!=null)
			{
				orderFromDb.OrderStatus = orderStatus;
				if (paymentStatus!=null)
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}

		public void UpdateStripePaymentId(int orderId, string seccionId, string paymentIntentId)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.Id == orderId);
			if (orderFromDb!=null)
			{
				orderFromDb.SessionId = seccionId;
				orderFromDb.PaymentIntentId = paymentIntentId;
			}
		}
	}
}
