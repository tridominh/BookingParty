using BirthdayParty.Models.DTOs;
using BirthdayParty.Models;

namespace BirthdayParty.Services.Interfaces
{
	public interface IUserService
	{
		List<User> GetAllUsers();
		User GetUserById(int id);
		User UpdateUser(UserUpdateDTO updatedUser);
		User DeleteUser(int id);
	}
}
