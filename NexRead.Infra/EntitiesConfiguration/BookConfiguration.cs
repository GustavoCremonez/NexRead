using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexRead.Domain.Entities;

namespace NexRead.Infra.EntitiesConfiguration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(5000);

        builder.Property(b => b.Isbn)
            .HasMaxLength(20);

        builder.Property(b => b.ImageUrl)
            .HasMaxLength(1000);

        builder.Property(b => b.Language)
            .HasMaxLength(10);

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        builder.Property(b => b.UpdatedAt);

        builder.HasIndex(b => b.Isbn);
    }
}
