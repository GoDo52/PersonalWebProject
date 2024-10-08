using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISpendingRepository Spending { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }

        void Save();
    }
}
