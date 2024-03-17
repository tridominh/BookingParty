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
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingPartyContext dbContext) : base(dbContext)
        {
        }
    }
}
