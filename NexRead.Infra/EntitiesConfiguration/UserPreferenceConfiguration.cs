using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexRead.Domain.Entities;

namespace NexRead.Infra.EntitiesConfiguration;

public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
{
    public void Configure(EntityTypeBuilder<UserPreference> builder)
    {
        builder.ToTable("UserPreferences");

        builder.HasKey(up => up.Id);

        builder.Property(up => up.Id)
            .ValueGeneratedOnAdd();

        builder.Property(up => up.PreferredMoods)
            .HasColumnType("text[]");

        builder.Property(up => up.PreferredLanguages)
            .HasColumnType("text[]");

        builder.Property(up => up.CreatedAt)
            .IsRequired();

        builder.Property(up => up.UpdatedAt);

        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPreferences)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
