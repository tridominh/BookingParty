using System;
using System.Collections.Generic;

namespace BirthdayParty.Models.ModelScaffold;

public abstract partial class ServiceImage<T>
{
    public int ServiceImageId { get; set; }

    public int ServiceId { get; set; }

    public T Image { get; set; }

    public virtual Service Service { get; set; }
}
