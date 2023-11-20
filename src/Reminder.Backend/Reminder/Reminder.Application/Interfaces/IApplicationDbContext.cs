using Microsoft.EntityFrameworkCore;
using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<DisposablePrompt> DisposablePrompts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}