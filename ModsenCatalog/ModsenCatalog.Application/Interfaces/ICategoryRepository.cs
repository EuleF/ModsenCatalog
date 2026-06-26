using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> GetByNameAsync(string name);
    
    Task<List<Category>> GetCategoriesWithProductsAsync();
}