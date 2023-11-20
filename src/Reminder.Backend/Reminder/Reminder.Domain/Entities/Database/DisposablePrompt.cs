namespace Reminder.Domain.Entities.Database;

public class DisposablePrompt : DatabaseEntity
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ShowsAt { get; set; } = DateTime.MaxValue;
    
    public User User { get; set; }
}