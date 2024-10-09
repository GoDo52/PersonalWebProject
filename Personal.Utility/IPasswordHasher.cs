using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Utility
{
	public interface IPasswordHasher
	{
		(string Hash, string Salt) HashPassword(string Password);
		bool VerifyPassword(string Hash, string Salt, string Password);
	}
}
