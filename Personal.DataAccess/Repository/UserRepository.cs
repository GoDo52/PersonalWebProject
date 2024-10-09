using Personal.DataAccess.Data;
using Personal.DataAccess.Exceptions;
using Personal.DataAccess.Repository.IRepository;
using Personal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private ApplicationDbContext _db;

		public UserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public override void Add(User user)
		{
            bool duplicateUserName = _db.Users.Any(u => u.UserName == user.UserName);
			bool duplicateEmail = _db.Users.Any(u => u.Email == user.Email);

			if (duplicateUserName)
            {
                throw new DuplicateUserNameException();
            }
            else if (duplicateEmail) 
            {
				throw new DuplicateUserEmailException();
			}
            else
            {
				base.Add(user);
			}
		}

		public void Update(User user)
		{
			var userFromDb = _db.Users.FirstOrDefault(s => s.Id == user.Id);

			if (userFromDb != null)
			{
				bool duplicateUserName = _db.Users.Any(u => u.UserName == user.UserName);
				bool duplicateEmail = _db.Users.Any(u => u.Email == user.Email);

				if (duplicateUserName && user.UserName != userFromDb.UserName)
				{
					throw new DuplicateUserNameException();
				}
				else if (duplicateEmail && user.Email != userFromDb.Email)
				{
					throw new DuplicateUserEmailException();
				}
				else
				{
					userFromDb.Id = user.Id;
					userFromDb.UserName = user.UserName;
					userFromDb.Email = user.Email;
					userFromDb.RoleId = user.RoleId;

					_db.Users.Update(userFromDb);
				}
			}
			else
			{
				throw new UserNotFoundException();
			}
        }
	}
}
