using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BirthdayParty.Models.ModelScaffold;

public partial class User : IdentityUser<int>
{
    //public string? ProfilePicture { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
