using System.Collections.Concurrent;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Events;

public class EventPublisher : IEventPublisher, IDisposable
{
    private readonly ConcurrentDictionary<Type, List<Delegate>> _subscriptions = new();
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed;

    public EventPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class
    {
        var eventType = typeof(TEvent);
        
        if (!_subscriptions.ContainsKey(eventType))
        {
            _subscriptions[eventType] = new List<Delegate>();
        }
        
        _subscriptions[eventType].Add(handler);
    }

    public void Publish<TEvent>(TEvent eventData) where TEvent : class
    {
        if (eventData == null)
        {
            throw new ArgumentNullException(nameof(eventData));
        }

        var eventType = typeof(TEvent);
        
        if (_subscriptions.TryGetValue(eventType, out var handlers))
        {
            foreach (var handler in handlers)
            {
                try
                {
                    ((Action<TEvent>)handler)?.Invoke(eventData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling event {eventType.Name}: {ex.Message}");
                }
            }
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _subscriptions.Clear();
            _disposed = true;
        }
    }
}