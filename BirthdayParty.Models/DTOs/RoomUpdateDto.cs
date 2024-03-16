namespace BirthdayParty.Models.DTOs
{
    public class RoomUpdateDto
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string RoomStatus { get; set; }
    }
}
