using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexRead.Domain.Entities;

namespace NexRead.Infra.EntitiesConfiguration;

public class UserLibraryConfiguration : IEntityTypeConfiguration<UserLibrary>
{
    public void Configure(EntityTypeBuilder<UserLibrary> builder)
    {
        builder.ToTable("UserLibraries");

        builder.HasKey(ul => ul.Id);

        builder.Property(ul => ul.Id)
            .ValueGeneratedOnAdd();

        builder.Property(ul => ul.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(ul => ul.AddedAt)
            .IsRequired();

        builder.Property(ul => ul.UpdatedAt);

        builder.HasOne(ul => ul.User)
            .WithMany(u => u.UserLibraries)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ul => ul.Book)
            .WithMany(b => b.UserLibraries)
            .HasForeignKey(ul => ul.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ul => new { ul.UserId, ul.BookId })
            .IsUnique();
    }
}
