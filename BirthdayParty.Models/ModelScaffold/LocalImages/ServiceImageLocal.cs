using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayParty.Models.ModelScaffold.LocalImages;

[Table("ServiceImage")]
public partial class ServiceImageLocal : ServiceImage<byte[]>
{
}
