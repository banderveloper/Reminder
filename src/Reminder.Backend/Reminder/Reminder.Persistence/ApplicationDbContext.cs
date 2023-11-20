using Microsoft.EntityFrameworkCore;
using Reminder.Application.Interfaces;
using Reminder.Domain.Entities.Database;
using Reminder.Persistence.EntityTypeConfigurations;

namespace Reminder.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<DisposablePrompt> DisposablePrompts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DisposablePromptTypeConfiguration());
    }
}