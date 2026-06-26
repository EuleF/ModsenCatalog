using System.Linq.Expressions;

namespace ModsenCatalog.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);

    Task<List<T>> GetAllAsync();

    Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(Guid id);
}