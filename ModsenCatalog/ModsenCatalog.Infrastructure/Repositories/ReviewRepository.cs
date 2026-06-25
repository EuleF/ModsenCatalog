using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(IMongoCollection<Review> collection) : base(collection)
    {
    }

    public List<Review> GetByProductId(Guid productId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.ProductId, productId);
        
        return _collection.Find(filter).ToList();
    }

    public List<Review> GetByUserId(Guid userId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.UserId, userId);
        
        return _collection.Find(filter).ToList();
    }

    public Review GetByUserAndProduct(Guid userId, Guid productId)
    {
        var filter = Builders<Review>.Filter.And(
            Builders<Review>.Filter.Eq(r => r.UserId, userId),
            Builders<Review>.Filter.Eq(r => r.ProductId, productId)
        );
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public List<Review> GetByRating(int rating)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.Rating, rating);
        
        return _collection.Find(filter).ToList();
    }

    public float GetAverageRatingForProduct(Guid productId)
    {
        var reviews = GetByProductId(productId);
        
        if (!reviews.Any())
        {
            return 0;
        }

        return (float)Math.Round(reviews.Average(r => r.Rating), 2);
    }

    public List<Review> GetRecentReviews(int count)
    {
        var sort = Builders<Review>.Sort.Descending(r => r.CreatedAt);
        
        return _collection.Find(_ => true)
            .Sort(sort)
            .Limit(count)
            .ToList();
    }

    public void DeleteByUserId(Guid userId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.UserId, userId);
        
        _collection.DeleteMany(filter);
    }

    public void DeleteByProductId(Guid productId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.ProductId, productId);
        
        _collection.DeleteMany(filter);
    }
}