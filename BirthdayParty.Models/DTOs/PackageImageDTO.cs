using Microsoft.AspNetCore.Http;

namespace BirthdayParty.Models.DTOs
{
    public class PackageImageDto
    {
        public int PackageImageId { get; set; }
        public int PackageId { get; set; }
        public IFormFile Image { get; set; }
    }
}
