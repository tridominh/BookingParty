using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BirthdayParty.DAL.ModelScaffold;

public partial class User : IdentityUser<int>
{
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
