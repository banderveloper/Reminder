using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reminder.Domain.Entities.Database;

namespace Reminder.Persistence.EntityTypeConfigurations;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(prop => prop.Username).IsUnique();

        builder.HasMany(prop => prop.DisposablePrompts)
            .WithOne(dp => dp.User)
            .HasForeignKey(dp => dp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}