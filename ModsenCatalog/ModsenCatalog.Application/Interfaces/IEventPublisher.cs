namespace ModsenCatalog.Application.Interfaces;

public interface IEventPublisher
{
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
    void Publish<TEvent>(TEvent eventData) where TEvent : class;
}