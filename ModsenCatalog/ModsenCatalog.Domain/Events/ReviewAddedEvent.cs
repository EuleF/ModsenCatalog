namespace ModsenCatalog.Domain.Events;

public class ReviewAddedEvent
{
    public Guid ReviewId { get; set; }
    public Guid ProductId { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}