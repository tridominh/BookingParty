﻿using System;
using System.Collections.Generic;

namespace BirthdayParty.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; }

    public decimal DepositMoney { get; set; }

    public int BookingId { get; set; }

    public virtual Booking Booking { get; set; }
}
