using Books2023.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Books2023.Web.ViewModels
{
    public class ProductEditVm
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoriesList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypesList { get; set; }
    }
}
