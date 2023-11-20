using System.ComponentModel.DataAnnotations;

namespace Reminder.Domain.Entities.Database;

public abstract class DatabaseEntity
{
    [Key]
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}