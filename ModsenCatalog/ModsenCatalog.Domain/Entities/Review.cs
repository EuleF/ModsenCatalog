namespace ModsenCatalog.Domain.Entities;

public class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public byte Rating { get; set; } = 1;
    public string Comment { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public Guid ProductId { get; set; } = Guid.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}