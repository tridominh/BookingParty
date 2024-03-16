using System;
using System.Collections.Generic;

namespace BirthdayParty.DAL.ModelScaffold;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
