namespace ModsenCatalog.Domain.Events;

public class UserRoleChangedEvent
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string OldRole { get; set; } = string.Empty;
    public string NewRole { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}