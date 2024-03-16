using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayParty.Models.LocalImages;

[Table("RoomImage")]
public partial class RoomImageLocal : RoomImage<byte[]>
{
}
