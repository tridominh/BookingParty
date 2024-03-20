namespace BirthdayParty.Models.DTOs
{
    public class BookingDTO 
    {
        public int? BookingId { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        //public DateTime BookingDate { get; set; }

        public DateTime PartyDateTime { get; set; }

        public DateTime PartyEndTime { get; set; }

        public decimal? TotalPrice { get; set; }

        public string? BookingStatus { get; set; }

        public string? Feedback { get; set; }

        public List<ServiceInBookingDTO>? ServiceIds { get; set; } = new List<ServiceInBookingDTO>();

        //public ICollection<string> PaymentIds { get; set; } = new List<string>();
    }

    public class ServiceInBookingDTO
    {
        public int ServiceId { get; set; }
        public int Amount { get; set; }
    }
}

