using System.Linq.Expressions;

namespace ModsenCatalog.Application.Interfaces;

public interface IRepository<T> where T : class
{
    T GetById(Guid id);

    List<T> GetAll();

    List<T> Find(Expression<Func<T, bool>> filter);

    void Create(T entity);

    void Update(T entity);

    void Delete(Guid id);
}