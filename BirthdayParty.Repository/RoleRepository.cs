using BirthdayParty.DAL;
using BirthdayParty.Models;
using ClassLibrary.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Repository
{
    public class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(BookingPartyContext dbContext) : base(dbContext)
        {
        }
    }
}
