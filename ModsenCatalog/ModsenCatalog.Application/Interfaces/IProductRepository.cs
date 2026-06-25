using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    List<Product> GetByCategoryId(Guid categoryId);

    List<Product> GetByPriceRange(decimal minPrice, decimal maxPrice);

    List<Product> GetTopRatedProducts(int count);

    List<Product> Search(string searchTerm);

    List<Product> GetPaged(int page, int pageSize);
        
    float GetAverageRating(Guid productId);
}