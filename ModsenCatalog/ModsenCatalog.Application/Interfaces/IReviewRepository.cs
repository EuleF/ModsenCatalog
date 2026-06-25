using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface IReviewRepository : IRepository<Review>
{
    List<Review> GetByProductId(Guid productId);

    List<Review> GetByUserId(Guid userId);

    Review GetByUserAndProduct(Guid userId, Guid productId);

    List<Review> GetByRating(int rating);
        
    float GetAverageRatingForProduct(Guid productId);
        
    List<Review> GetRecentReviews(int count);

    void DeleteByUserId(Guid userId);
        
    void DeleteByProductId(Guid productId);
}