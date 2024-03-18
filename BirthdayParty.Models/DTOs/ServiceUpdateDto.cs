namespace BirthdayParty.Models.DTOs
{
    public class ServiceUpdateDto
    {
        public int ServiceId { get; set; }
        public int PackageId { get; set; }
        public decimal ServicePrice { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; }
    }
}
