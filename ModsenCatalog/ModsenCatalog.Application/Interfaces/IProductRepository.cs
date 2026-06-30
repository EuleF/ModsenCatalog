using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetByCategoryIdAsync(Guid categoryId);

    Task<List<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);

    Task<List<Product>> GetTopRatedProductsAsync(int count);

    Task<List<Product>> SearchAsync(string searchTerm);

    Task<List<Product>> GetPagedAsync(int page, int pageSize);
        
    Task<float> GetAverageRatingAsync(Guid productId);
}