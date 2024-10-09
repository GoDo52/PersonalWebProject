using Personal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Services
{
	public interface IUserService
	{
		void RegisterUser(UserRegistration userRegistration);
		bool VerifyUserOnLogin(string email, string password, out User? user);
		void UpdateUser(AccountEdit accountEdit, string currentEmail, string currentUserName, out User? user);

    }
}
