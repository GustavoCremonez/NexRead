using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexRead.Domain.Entities;

namespace NexRead.Infra.EntitiesConfiguration;

public class UserPreferredGenreConfiguration : IEntityTypeConfiguration<UserPreferredGenre>
{
    public void Configure(EntityTypeBuilder<UserPreferredGenre> builder)
    {
        builder.ToTable("UserPreferredGenres");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPreferredGenres)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Genre)
            .WithMany(u => u.UserPreferredGenres)
            .HasForeignKey(up => up.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
