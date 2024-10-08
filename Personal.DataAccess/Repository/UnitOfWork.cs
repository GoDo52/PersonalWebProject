using Personal.DataAccess.Data;
using Personal.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public ISpendingRepository Spending { get; private set; }
        public IUserRepository User { get; private set; }
        public IRoleRepository Role { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Spending = new SpendingRepository(_db);
            User = new UserRepository(_db);
            Role = new RoleRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
