using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException() : base("User was not found in the Data Base whilst updating!") { }
	}
}
