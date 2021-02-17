using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lisans.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext DbContext;
        private readonly DbSet<T> DbSet;

        /// <summary>
        /// Repository instance ı başlatır.
        /// </summary>
        /// <param name="dbContext">Veritabanı bağlantı nesnesi</param>
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet
            .Where(predicate);
            return iQueryable.Count();
        }

        public void Delete(T entity)
        {
            // Önce entity'nin state'ini kontrol etmeliyiz.
            EntityEntry<T> dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet
               .Where(predicate);
            return iQueryable.ToList().FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = DbSet.Where(x => x != null);
            return iQueryable;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet
               .Where(predicate);
            return iQueryable;
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
