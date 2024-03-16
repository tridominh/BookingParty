using System;
using System.Collections.Generic;
using BirthdayParty.Models.LocalImages;

namespace BirthdayParty.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; }

    public int Capacity { get; set; }

    public string RoomStatus { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<RoomImageLocal> RoomImages { get; set; } = new List<RoomImageLocal>();
}
