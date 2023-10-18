using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Books2023.Models.Models
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200,ErrorMessage ="Must be between {2} and {1}",MinimumLength =3)]
        public string Title { get; set; }
        [MaxLength(356, ErrorMessage = "Must have at least {1} characters")]

        public string? Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Must be between {2} and {1}", MinimumLength = 3)]
        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 1-50")]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 51-100")]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 10000)]
        public double Price100 { get; set; }
        [Display(Name ="Image")]
        [ValidateNever]

        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]

        public Category Category { get; set; }

        [Required]
        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]

        public CoverType CoverType { get; set; }
    }
}
