using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repo
{
    public interface IGenericRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
    }
}
