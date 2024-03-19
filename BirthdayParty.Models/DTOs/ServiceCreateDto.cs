using System.ComponentModel.DataAnnotations;

namespace BirthdayParty.Models.DTOs
{
    public class ServiceCreateDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Package Id must be a positive number")]
        public int PackageId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        [Required(ErrorMessage = "Price is required")]
        public decimal ServicePrice { get; set; }
        [Required(ErrorMessage = "Service Name is required")]
        public string ServiceName { get; set; } = null!;

        public string ServiceDescription { get; set; }
    }
}
