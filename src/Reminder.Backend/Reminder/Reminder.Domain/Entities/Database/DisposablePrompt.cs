using System.Text.Json.Serialization;

namespace Reminder.Domain.Entities.Database;

// Reminding prompt, disappears after showing
public class DisposablePrompt : DatabaseEntity
{
    // Id of user (author of prompt), FK 
    public long UserId { get; set; }
    
    // Title of reminding
    public string Title { get; set; }
    
    // Detailed description
    public string? Description { get; set; }
    
    // Time of showing prompt to user in client
    public DateTime ShowsAt { get; set; } = DateTime.MaxValue;
    
    // Foreign key to user
    [JsonIgnore]
    public User User { get; set; }
}