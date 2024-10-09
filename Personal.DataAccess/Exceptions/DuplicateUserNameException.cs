using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Exceptions
{
	public class DuplicateUserNameException : Exception
	{
		public DuplicateUserNameException() : base("This user name is already taken!") { }
	}
}
