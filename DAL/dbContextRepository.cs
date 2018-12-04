using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class dbContextRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        RealEstateDB _context;
        DbSet<TEntity> _dbSet;

        public dbContextRepository(RealEstateDB context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public virtual TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Create(TEntity item)
        {
            _dbSet.Add(item);
            //_context.SaveChanges();
        }
        public virtual void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            //_context.SaveChanges();
        }
        public virtual void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            //_context.SaveChanges();
        }
        public virtual IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public virtual IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
