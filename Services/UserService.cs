﻿using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;

namespace BirthdayParty.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;

		public UserService(IUserRepository user)
		{
			this.userRepository = user;
		}

		public List<User> GetAllUsers()
		{
			return userRepository.GetAll().ToList();
		}

		public User GetUserById(int id)
		{
			return userRepository.Get(id);
		}

		public User UpdateUser(UserUpdateDTO updatedUser)
		{
			var user = userRepository.Get(updatedUser.Id);

			user.Id = updatedUser.Id;
			user.UserName = updatedUser.UserName;
			user.PhoneNumber = updatedUser.PhoneNumber;
			user.Email = updatedUser.Email;
			return userRepository.Update(user);
		}

		public User DeleteUser(int id)
		{
			return userRepository.Delete(id);
		}

	}
}