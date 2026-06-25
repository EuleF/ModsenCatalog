using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Category GetByName(string name);
    
    List<Category> GetCategoriesWithProducts();
}