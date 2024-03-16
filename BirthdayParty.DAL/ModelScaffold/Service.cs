using System;
using System.Collections.Generic;

namespace BirthdayParty.DAL.ModelScaffold;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public int Capacity { get; set; }

    public int PackageId { get; set; }

    public TimeSpan PartyTime { get; set; }

    public virtual Package Package { get; set; } = null!;
}
