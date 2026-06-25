using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoCollection<Category> collection) : base(collection)
    {
    }

    public Category GetByName(string name)
    {
        var filter = Builders<Category>.Filter.Eq(c => c.Name, name);
        return _collection.Find(filter).FirstOrDefault();
    }

    public List<Category> GetCategoriesWithProducts()
    {
        return _collection.Find(_ => true).ToList();
    }
}