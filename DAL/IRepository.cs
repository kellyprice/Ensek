using System.Linq.Expressions;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Create(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        void Delete();
    }
}
