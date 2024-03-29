using BirthdayParty.Models.LocalImages;
using System.ComponentModel.DataAnnotations;

namespace BirthdayParty.Models.DTOs
{
    public class RoomUpdateDto
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "RoomNumber is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "RoomNumber must be a number")]
        [MaxLength(10, ErrorMessage = "RoomNumber cannot exceed 10 characters")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Price must be a number")]
        [Range(1, 2000000, ErrorMessage = "Price must be between 1 and 2.000.000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Capacity must be a number")]
        [Range(1, 200, ErrorMessage = "Capacity must be between 1 and 200")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "RoomStatus is required")]
        [MaxLength(50, ErrorMessage = "RoomStatus cannot exceed 50 characters")]
        public string RoomStatus { get; set; }
    }
}
