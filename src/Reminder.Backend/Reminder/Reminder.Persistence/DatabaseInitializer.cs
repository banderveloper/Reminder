namespace Reminder.Persistence;

// Invokes in main during server starting
public static class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
    }
}