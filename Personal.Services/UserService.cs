using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
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

			User newUser = new User
			{
				UserName = userRegistration.UserName,
				Email = userRegistration.Email,
				PasswordHash = passwordHash.Hash,
				PasswordSalt = passwordHash.Salt,
				RoleId = SD.DefaultRoleId
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
	}
}
