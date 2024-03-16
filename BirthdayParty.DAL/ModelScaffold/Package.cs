﻿using System;
using System.Collections.Generic;

namespace BirthdayParty.DAL.ModelScaffold;

public partial class Package
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public string PackageType { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
