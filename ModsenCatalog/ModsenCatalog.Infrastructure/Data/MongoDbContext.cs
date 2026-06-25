using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Infrastructure.Options;

namespace ModsenCatalog.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoDbOptions options)
    {
        var client = new MongoClient(options.ConnectionString);
        _database = client.GetDatabase(options.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    public IMongoCollection<Review> Reviews => _database.GetCollection<Review>("Reviews");
}