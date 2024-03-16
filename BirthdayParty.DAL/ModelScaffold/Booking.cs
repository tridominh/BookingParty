using System;
using System.Collections.Generic;

namespace BirthdayParty.DAL.ModelScaffold;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int PackageId { get; set; }

    public DateTime BookingDate { get; set; }

    public int TotalPrice { get; set; }

    public int PaymentId { get; set; }

    public int RoomId { get; set; }

    public virtual Package Package { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
