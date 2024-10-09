using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T entity);
        /*  
            Why there is no implementation of Update and Save methods here:
            When you update one model logic might be different from updating another model,
            thus no implementation here. But when making, for example, CategoryRepository,
            there will be an implemantation of Update and Save methods 
        */
        // void Update(T entity);
        // void Save();
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
