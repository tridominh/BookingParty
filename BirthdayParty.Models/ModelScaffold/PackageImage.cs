using System;
using System.Collections.Generic;

namespace BirthdayParty.Models.ModelScaffold;

public abstract partial class PackageImage<T>
{
    public int PackageImageId { get; set; }

    public int PackageId { get; set; }

    public T Image { get; set; }

    public virtual Package Package { get; set; }
}
