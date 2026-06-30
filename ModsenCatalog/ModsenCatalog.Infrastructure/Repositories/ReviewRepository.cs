using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(IMongoCollection<Review> collection) : base(collection)
    {
    }

    public async Task<List<Review>> GetByProductIdAsync(Guid productId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.ProductId, productId);
        
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<List<Review>> GetByUserIdAsync(Guid userId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.UserId, userId);
        
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<Review> GetByUserAndProductAsync(Guid userId, Guid productId)
    {
        var filter = Builders<Review>.Filter.And(
            Builders<Review>.Filter.Eq(r => r.UserId, userId),
            Builders<Review>.Filter.Eq(r => r.ProductId, productId)
        );
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Review>> GetByRatingAsync(int rating)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.Rating, rating);
        
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<float> GetAverageRatingForProductAsync(Guid productId)
    {
        var reviews = await GetByProductIdAsync(productId);
        
        if (!reviews.Any())
        {
            return 0;
        }

        return (float)Math.Round(reviews.Average(r => r.Rating), 2);
    }

    public async Task<List<Review>> GetRecentReviewsAsync(int count)
    {
        var sort = Builders<Review>.Sort.Descending(r => r.CreatedAt);
        
        return await _collection.Find(_ => true)
            .Sort(sort)
            .Limit(count)
            .ToListAsync();
    }

    public async Task DeleteByUserIdAsync(Guid userId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.UserId, userId);
        
        await _collection.DeleteManyAsync(filter);
    }

    public async Task DeleteByProductIdAsync(Guid productId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.ProductId, productId);
        
        await _collection.DeleteManyAsync(filter);
    }
}