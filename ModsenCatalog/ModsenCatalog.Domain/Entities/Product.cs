namespace ModsenCatalog.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public float AverageRating { get; set; } = 0;
    public Guid CategoryId { get; set; } = Guid.Empty;
    public Category Category { get; set; } = new Category();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}