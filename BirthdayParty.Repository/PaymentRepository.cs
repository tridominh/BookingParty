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
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository(BookingPartyContext dbContext) : base(dbContext)
        {
        }
    }
}
