using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Books2023.Models.Models
{

    [Index(nameof(Name), IsUnique = true)]

    public class Company
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The field {0} must be between {2} y {1} characters", MinimumLength = 3)]

        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
