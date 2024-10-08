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
	public class RoleRepository : Repository<Role>, IRoleRepository
	{
		private readonly ApplicationDbContext _db;

		public RoleRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Role role)
		{
			_db.Roles.Update(role);
		}
	}
}
