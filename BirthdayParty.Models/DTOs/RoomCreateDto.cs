namespace BirthdayParty.Models.DTOs
{
    public class RoomCreateDto
    {
        public string RoomNumber { get; set; }
        
        public decimal Price { get; set; }

        public int Capacity { get; set; }
    }
}
