using System;
using System.Collections.Generic;

namespace BirthdayParty.DAL.ModelScaffold;

public partial class Room
{
    public int RoomId { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
