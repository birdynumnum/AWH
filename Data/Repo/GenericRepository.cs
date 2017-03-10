using Data.Helpers;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repo
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext dataContext;
        private readonly IDbSet<T> DbSet;

        protected GenericRepository(ApplicationDbContext context)
        {
            dataContext = context;
            DbSet = dataContext.Set<T>();
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(T entity)
        {
            dataContext.Entry(entity).State =  StateHelper.ConvertState(State.Modified);
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dataContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
}
