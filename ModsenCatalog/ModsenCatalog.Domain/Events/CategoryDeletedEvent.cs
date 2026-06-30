namespace ModsenCatalog.Domain.Events;

public class CategoryDeletedEvent
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}