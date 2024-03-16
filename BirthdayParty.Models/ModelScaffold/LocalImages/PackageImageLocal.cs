using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayParty.Models.ModelScaffold.LocalImages;

[Table("PackageImage")]
public partial class PackageImageLocal : PackageImage<byte[]>
{
}
