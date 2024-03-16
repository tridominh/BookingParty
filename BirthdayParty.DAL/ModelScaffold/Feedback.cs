using System;
using System.Collections.Generic;

namespace BirthdayParty.DAL.ModelScaffold;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string Description { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
