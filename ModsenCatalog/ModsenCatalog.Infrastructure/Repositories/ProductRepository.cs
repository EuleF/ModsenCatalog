using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(IMongoCollection<Product> collection) : base(collection)
    {
    }

    public async Task<List<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<List<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var filter = Builders<Product>.Filter.And(
            Builders<Product>.Filter.Gte(p => p.Price, minPrice),
            Builders<Product>.Filter.Lte(p => p.Price, maxPrice)
        );
        
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<List<Product>> GetTopRatedProductsAsync(int count)
    {
        var sort = Builders<Product>.Sort.Descending(p => p.AverageRating);
        
        return await _collection.Find(_ => true)
            .Sort(sort)
            .Limit(count)
            .ToListAsync();
    }

    public async Task<List<Product>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        var filter = Builders<Product>.Filter.Or(
            Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
            Builders<Product>.Filter.Regex(p => p.Description, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
        );
        
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<List<Product>> GetPagedAsync(int page, int pageSize)
    {
        return await _collection.Find(_ => true)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<float> GetAverageRatingAsync(Guid productId)
    {
        var product = await GetByIdAsync(productId);
        return product?.AverageRating ?? 0;
    }
}