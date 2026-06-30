using ModsenCatalog.Application.Interfaces;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Domain.Events;

namespace ModsenCatalog.Infrastructure.Services;

public class EventService : IEventService
{
    private readonly IEventPublisher _eventPublisher;

    public EventService(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public void PublishPriceChanged(Product product, decimal oldPrice)
    {
        _eventPublisher.Publish(new PriceChangedEvent
        {
            ProductId = product.Id,
            ProductName = product.Name,
            OldPrice = oldPrice,
            NewPrice = product.Price,
            OccurredOn = DateTime.UtcNow
        });
    }

    public void PublishProductDeleted(Product product)
    {
        _eventPublisher.Publish(new ProductDeletedEvent
        {
            ProductId = product.Id,
            ProductName = product.Name,
            OccurredOn = DateTime.UtcNow
        });
    }

    public void PublishReviewAdded(Review review, User user)
    {
        _eventPublisher.Publish(new ReviewAddedEvent
        {
            ReviewId = review.Id,
            ProductId = review.ProductId,
            Username = user.Username,
            OccurredOn = DateTime.UtcNow
        });
    }

    public void PublishReviewDeleted(Review review, User user)
    {
        _eventPublisher.Publish(new ReviewDeletedEvent
        {
            ReviewId = review.Id,
            ProductId = review.ProductId,
            Username = user.Username,
            OccurredOn = DateTime.UtcNow
        });
    }

    public void PublishUserRoleChanged(User user, string oldRole, string newRole)
    {
        _eventPublisher.Publish(new UserRoleChangedEvent
        {
            UserId = user.Id,
            Username = user.Username,
            OldRole = oldRole,
            NewRole = newRole,
            OccurredOn = DateTime.UtcNow
        });
    }

    public void PublishUserLoggedIn(User user)
    {
        _eventPublisher.Publish(new UserLoggedInEvent
        {
            UserId = user.Id,
            Username = user.Username,
            OccurredOn = DateTime.UtcNow
        });
    }

    public void PublishCategoryDeleted(Category category)
    {
        _eventPublisher.Publish(new CategoryDeletedEvent
        {
            CategoryId = category.Id,
            CategoryName = category.Name,
            OccurredOn = DateTime.UtcNow
        });
    }
}