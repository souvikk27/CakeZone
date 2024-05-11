using System.Linq.Expressions;

namespace CakeZone.Common.Repository;

public interface IRepository<T>
{
    Task<T> GetById(object id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task Remove(T entity);
    Task SaveAsync();
}