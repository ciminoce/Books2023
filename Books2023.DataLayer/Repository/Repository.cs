using Books2023.DataLayer.Repository.Interfaces;
using Books2023.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Books2023.DataLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;//work with the correctly dbset.        public Repository(ApplicationDbContext db)

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet=_db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? propertiesNames = null, bool tracked = true)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = _dbSet;
            }
            else
            {
                query = _dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (propertiesNames != null)
            {
                var properties = propertiesNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? propertiesNames = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            if (propertiesNames!=null)
            {
                var properties = propertiesNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach ( var property in properties)
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();

        }

		public void RemoveRange(IEnumerable<T> entities)
		{
            _dbSet.RemoveRange(entities);
		}
	}
}
