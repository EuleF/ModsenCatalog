using ModsenCatalog.Application.Interfaces;
using ModsenCatalog.Domain.Events;

namespace ModsenCatalog.Application.Handlers;

public class EventHandlerService
{
    private readonly IEventPublisher _eventPublisher;

    public EventHandlerService(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
        SubscribeAllHandlers();
    }

    private void SubscribeAllHandlers()
    {
        _eventPublisher.Subscribe<PriceChangedEvent>(HandlePriceChanged);
        
        _eventPublisher.Subscribe<ProductDeletedEvent>(HandleProductDeleted);
        
        _eventPublisher.Subscribe<ReviewAddedEvent>(HandleReviewAdded);
        
        _eventPublisher.Subscribe<ReviewDeletedEvent>(HandleReviewDeleted);
        
        _eventPublisher.Subscribe<UserRoleChangedEvent>(HandleUserRoleChanged);
        
        _eventPublisher.Subscribe<UserLoggedInEvent>(HandleUserLoggedIn);
        
        _eventPublisher.Subscribe<CategoryDeletedEvent>(HandleCategoryDeleted);
    }

    private void HandlePriceChanged(PriceChangedEvent eventData)
    {
        Console.WriteLine($"[EVENT] Цена товара '{eventData.ProductName}' изменена с {eventData.OldPrice:C} на {eventData.NewPrice:C}");
    }

    private void HandleProductDeleted(ProductDeletedEvent eventData)
    {
        Console.WriteLine($"[EVENT] Продукт '{eventData.ProductName}' удален");
    }

    private void HandleReviewAdded(ReviewAddedEvent eventData)
    {
        Console.WriteLine("[EVENT] Спасибо за отзыв!");
    }

    private void HandleReviewDeleted(ReviewDeletedEvent eventData)
    {
        Console.WriteLine("[EVENT] Ваш отзыв удален!");
    }

    private void HandleUserRoleChanged(UserRoleChangedEvent eventData)
    {
        Console.WriteLine($"[EVENT] Роль пользователя '{eventData.Username}' изменена с {eventData.OldRole} на {eventData.NewRole}");
    }

    private void HandleUserLoggedIn(UserLoggedInEvent eventData)
    {
        Console.WriteLine($"[EVENT] Добро пожаловать, {eventData.Username}!");
    }

    private void HandleCategoryDeleted(CategoryDeletedEvent eventData)
    {
        Console.WriteLine($"[EVENT] Категория '{eventData.CategoryName}' удалена");
    }
}