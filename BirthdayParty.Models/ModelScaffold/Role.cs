using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BirthdayParty.Models.ModelScaffold;

public partial class Role : IdentityRole<int>
{
    public Role() : base()
    { 
    }

    public Role(string roleName)
    {
        Name = roleName;
    }
}
