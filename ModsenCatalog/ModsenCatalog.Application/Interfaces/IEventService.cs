using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Domain.Events;

namespace ModsenCatalog.Application.Interfaces;

public interface IEventService
{
    void PublishPriceChanged(Product product, decimal oldPrice);
    void PublishProductDeleted(Product product);
    void PublishReviewAdded(Review review, User user);
    void PublishReviewDeleted(Review review, User user);
    void PublishUserRoleChanged(User user, string oldRole, string newRole);
    void PublishUserLoggedIn(User user);
    void PublishCategoryDeleted(Category category);
}