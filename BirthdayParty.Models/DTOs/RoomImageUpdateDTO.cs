using Microsoft.AspNetCore.Http;

namespace BirthdayParty.Models.DTOs
{
    public class RoomImageDto
    {
        public int? RoomImageId { get; set; }
        public int RoomId { get; set; }
        public IFormFile Image { get; set; }
    }
}
