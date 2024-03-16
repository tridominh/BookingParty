using BirthdayParty.DAL;
using BirthdayParty.Models;
using BirthdayParty.Repository.Interfaces;
using ClassLibrary.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParty.Repository
{
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(BookingPartyContext dbContext) : base(dbContext)
        {
        }
    }
}
