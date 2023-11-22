using System.Text.Json;

namespace Reminder.Domain.Entities.Cache;

// Parent of each cache-stored entities
public abstract class CacheEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public DateTime DestroysAt { get; set; } = DateTime.MaxValue;
}