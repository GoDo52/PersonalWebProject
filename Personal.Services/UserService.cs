using Microsoft.AspNetCore.Identity;
using Personal.DataAccess.Exceptions;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using Personal.Services.Interfaces;
using Personal.Utility;

namespace Personal.Services
{
    public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPasswordHasher _passwordHasher;

		public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
		{
			_unitOfWork = unitOfWork;
			_passwordHasher = passwordHasher;
		}

		public void RegisterUser (UserRegistration userRegistration)
		{
			var passwordHash = _passwordHasher.HashPassword(userRegistration.Password);

			int userRoleId = _unitOfWork.Role.Get(u => u.Name == SD.UserRole).Id;

			User newUser = new User
			{
				UserName = userRegistration.UserName,
				Email = userRegistration.Email,
				PasswordHash = passwordHash.Hash,
				PasswordSalt = passwordHash.Salt,
				RoleId = userRoleId
            };

			_unitOfWork.User.Add(newUser);
			_unitOfWork.Save();
		}

		public bool VerifyUserOnLogin (string email, string password, out User? user)
		{
			user = _unitOfWork.User.Get(u => u.Email == email);

			if (user == null)
			{
				return false;
			}
			else
			{
				return _passwordHasher.VerifyPassword(user.PasswordHash, user.PasswordSalt, password);
			}
		}

		public void UpdateUser (AccountEdit accountEdit, string currentEmail, string currentUserName, out User? user)
		{
            User userByEmail = _unitOfWork.User.Get(u => u.Email == accountEdit.Email);
			if (userByEmail != null && userByEmail.Email != currentEmail)
			{
				throw new DuplicateUserEmailException();
			}

            User userByUserName = _unitOfWork.User.Get(u => u.UserName == accountEdit.UserName);
			if (userByUserName.UserName != null && userByUserName.UserName != currentUserName)
            {
                throw new DuplicateUserEmailException();
            }

            user = _unitOfWork.User.Get(u => u.Email == currentEmail);
            
            if (accountEdit.Password != null)
			{
                var passwordHash = _passwordHasher.HashPassword(accountEdit.Password);

				user.PasswordHash = passwordHash.Hash;
				user.PasswordSalt = passwordHash.Salt;
            }

			user.UserName = accountEdit.UserName;
			user.Email = accountEdit.Email;
			user.PasswordHash = user.PasswordHash;
			user.PasswordSalt = user.PasswordSalt;
			user.RoleId = user.RoleId;

            _unitOfWork.User.Update(user);
            _unitOfWork.Save();
		}
	}
}
