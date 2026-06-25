using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(IMongoCollection<Product> collection) : base(collection)
    {
    }

    public List<Product> GetByCategoryId(Guid categoryId)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
        
        return _collection.Find(filter).ToList();
    }

    public List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice)
    {
        var filter = Builders<Product>.Filter.And(
            Builders<Product>.Filter.Gte(p => p.Price, minPrice),
            Builders<Product>.Filter.Lte(p => p.Price, maxPrice)
        );
        
        return _collection.Find(filter).ToList();
    }

    public List<Product> GetTopRatedProducts(int count)
    {
        var sort = Builders<Product>.Sort.Descending(p => p.AverageRating);
        
        return _collection.Find(_ => true)
            .Sort(sort)
            .Limit(count)
            .ToList();
    }

    public List<Product> Search(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return GetAll();

        var filter = Builders<Product>.Filter.Or(
            Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
            Builders<Product>.Filter.Regex(p => p.Description, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
        );
        
        return _collection.Find(filter).ToList();
    }

    public List<Product> GetPaged(int page, int pageSize)
    {
        return _collection.Find(_ => true)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToList();
    }

    public float GetAverageRating(Guid productId)
    {
        var product = GetById(productId);
        return product?.AverageRating ?? 0;
    }
}