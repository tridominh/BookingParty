using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Models.DTOs
{
    public class PackageCreateDto
    {
        [Required(ErrorMessage = "Package Name is required")]
        public string PackageName { get; set; }
        [Required(ErrorMessage = "Package Type is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Package Type should contain only alphabetical letters.")]
        public string PackageType { get; set; }
    }
}
