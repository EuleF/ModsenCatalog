using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetByProductIdAsync(Guid productId);

    Task<List<Review>> GetByUserIdAsync(Guid userId);

    Task<Review> GetByUserAndProductAsync(Guid userId, Guid productId);

    Task<List<Review>> GetByRatingAsync(int rating);
        
    Task<float> GetAverageRatingForProductAsync(Guid productId);
        
    Task<List<Review>> GetRecentReviewsAsync(int count);

    Task DeleteByUserIdAsync(Guid userId);
        
    Task DeleteByProductIdAsync(Guid productId);
}