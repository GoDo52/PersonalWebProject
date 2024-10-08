using Personal.DataAccess.Data;
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

		public void Update(User user)
		{
            var userFromDb = _db.Users.FirstOrDefault(s => s.Id == user.Id);
            if (userFromDb != null)
            {
                userFromDb.Id = user.Id;
                userFromDb.UserName = user.UserName;
                userFromDb.Email = user.Email;
                userFromDb.RoleId = user.RoleId;
                
                _db.Users.Update(userFromDb);
            }
            else
            {
                _db.Users.Update(user);
            }
        }
	}
}
