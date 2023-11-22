using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reminder.Domain.Entities.Database;

namespace Reminder.Persistence.EntityTypeConfigurations;

// EntityFramework DisposablePrompt entity configuration
public class DisposablePromptTypeConfiguration : IEntityTypeConfiguration<DisposablePrompt>
{
    public void Configure(EntityTypeBuilder<DisposablePrompt> builder)
    {
        builder.HasIndex(dp => dp.UserId);

        builder.HasOne(dp => dp.User)
            .WithMany(user => user.DisposablePrompts)
            .HasForeignKey(dp => dp.UserId);
    }
}