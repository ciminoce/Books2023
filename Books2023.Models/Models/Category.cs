using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books2023.Models.Models
{
    [Index(nameof(Name),IsUnique = true)]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The field {0} must be between {2} y {1} characters", MinimumLength = 3)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "The field {0} must be between {1} y {2}")]
        public int DisplayOrder { get; set; }
    }
}
