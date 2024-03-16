using System;
using System.Collections.Generic;

namespace BirthdayParty.Models.ModelScaffold;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; }

    public int Capacity { get; set; }

    public string RoomStatus { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
