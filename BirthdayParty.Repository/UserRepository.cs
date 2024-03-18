using BirthdayParty.DAL;
using BirthdayParty.Models;
using BirthdayParty.Repository.Interfaces;

namespace BirthdayParty.Repository
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(BookingPartyContext dbContext) : base(dbContext)
		{

		}
	}
}
