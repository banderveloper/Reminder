using Microsoft.EntityFrameworkCore;
using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Interfaces;

// Parent of applicationDbContext
public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<DisposablePrompt> DisposablePrompts { get; set; }
    
    // Method from DbContext
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}