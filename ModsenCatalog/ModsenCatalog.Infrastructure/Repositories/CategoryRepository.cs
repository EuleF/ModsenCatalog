using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoCollection<Category> collection) : base(collection)
    {
    }

    public async Task<Category> GetByNameAsync(string name)
    {
        var filter = Builders<Category>.Filter.Eq(c => c.Name, name);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Category>> GetCategoriesWithProductsAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}