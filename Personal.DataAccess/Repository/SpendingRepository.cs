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
    public class SpendingRepository : Repository<Spending>, ISpendingRepository
    {
        private ApplicationDbContext _db;

        public SpendingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Spending obj)
        {
            var spendingFromDb = _db.Spendings.FirstOrDefault(s => s.Id == obj.Id);
            if (spendingFromDb != null)
            {
                spendingFromDb.UserId = obj.UserId;
                spendingFromDb.CategoryId = obj.CategoryId;
                spendingFromDb.Amount = obj.Amount;
                // DateTime is why Update method is not in the generic Repository
                spendingFromDb.DateTime = obj.DateTime;
                spendingFromDb.Description = obj.Description;

                _db.Spendings.Update(spendingFromDb);
            }
            else
            {
                _db.Spendings.Update(obj);
            }

        }
    }
}
