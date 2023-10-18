using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books2023.Models.Models
{
    [Index(nameof(Name), IsUnique = true)]

    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The field {0} must be between {2} y {1} characters", MinimumLength = 3)]
        [DisplayName("Cover Type")]
        public string Name { get; set; }

    }
}
