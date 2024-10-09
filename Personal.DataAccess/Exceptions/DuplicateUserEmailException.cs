using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Exceptions
{
	public class DuplicateUserEmailException : Exception
	{
		public DuplicateUserEmailException() : base("This email is already taken!") { }
	}
}
