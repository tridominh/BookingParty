using Microsoft.AspNetCore.Http;

namespace BirthdayParty.Models.DTOs
{
    public class ServiceImageDto
    {
        public int ServiceImageId { get; set; }
        public int ServiceId { get; set; }
        public IFormFile Image { get; set; }
    }
}
