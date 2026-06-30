namespace ModsenCatalog.Domain.Events;

public class UserLoggedInEvent
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}