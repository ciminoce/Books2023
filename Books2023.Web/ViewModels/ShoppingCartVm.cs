using Books2023.Models.Models;

namespace Books2023.Web.ViewModels
{
    public class ShoppingCartVm
    {
        public IEnumerable<ShoppingCart> CartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
