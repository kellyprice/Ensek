using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>()
                .AsNoTracking();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>()
                .Where(expression)
                .AsNoTracking();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Create(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entities = Get(predicate);
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public void Delete()
        {
            _context.Set<T>().RemoveRange();
        }
    }
}
