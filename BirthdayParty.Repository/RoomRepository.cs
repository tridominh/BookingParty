using BirthdayParty.DAL;
using BirthdayParty.Models;
using BirthdayParty.Repository.Interfaces;
using ClassLibrary.Repository.Implementation;

namespace BirthdayParty.Repository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingPartyContext dbContext) : base(dbContext)
        {
        }
    }
}
