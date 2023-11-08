using Books2023.Models.Models;

namespace Books2023.Web.ViewModels
{
	public class OrderListVm
	{
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
