using System.Linq.Expressions;

namespace DataAccess.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> CreateAsync(T entity);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
    }
}
