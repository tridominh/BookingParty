using System;
using System.Collections.Generic;

namespace BirthdayParty.Models;

public abstract partial class RoomImage<T>
{
    public int RoomImageId { get; set; }

    public int RoomId { get; set; }

    public T Image { get; set; }

    public virtual Room Room { get; set; }
}
