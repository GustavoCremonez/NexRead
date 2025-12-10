using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexRead.Domain.Entities;

namespace NexRead.Infra.EntitiesConfiguration;

public class UserPreferredAuthorConfiguration : IEntityTypeConfiguration<UserPreferredAuthor>
{
    public void Configure(EntityTypeBuilder<UserPreferredAuthor> builder)
    {
        builder.ToTable("UserPreferredAuthors");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPreferredAuthors)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Author)
            .WithMany(u => u.UserPreferredAuthors)
            .HasForeignKey(up => up.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
