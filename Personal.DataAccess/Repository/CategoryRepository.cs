using Personal.DataAccess.Repository.IRepository;
using Personal.DataAccess.Repository;
using Personal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personal.DataAccess.Data;
using Personal.DataAccess.Exceptions;

namespace Personal.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public override void Add(Category obj)
        {
            if (_db.Categories.Any(c => c.Name == obj.Name))
            {
                throw new DuplicateCategoryException();
            }
            base.Add(obj);
        }

        public void Update(Category obj)
        {
            if (_db.Categories.Any(c => c.Name == obj.Name))
            {
                throw new DuplicateCategoryException();
            }
            _db.Categories.Update(obj);
        }
    }
}
