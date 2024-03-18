using BirthdayParty.Repository.Interfaces;
using BirthdayParty.DAL;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookingPartyContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(BookingPartyContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Delete(params int[] keys)
        {
            var entity = Get(keys);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<T> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query = include(query);
                }
            }

            return query.ToList();
        }

        public T Get(params int[] keys)
        {
            if (keys.Length == 1)
            {
                return _dbSet.Find(keys[0]);
            }
            var entity = _dbSet.Find(keys[0], keys[1]);
            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
