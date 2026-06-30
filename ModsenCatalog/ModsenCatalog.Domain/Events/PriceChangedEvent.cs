namespace ModsenCatalog.Domain.Events;

public class PriceChangedEvent
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}