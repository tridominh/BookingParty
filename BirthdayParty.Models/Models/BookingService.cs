﻿using System;
using System.Collections.Generic;

namespace BirthdayParty.Models;

public partial class BookingService
{
    public int BookingServiceId { get; set; }

    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public int? Amount { get; set; }

    public virtual Booking Booking { get; set; }

    public virtual Service Service { get; set; }
}
