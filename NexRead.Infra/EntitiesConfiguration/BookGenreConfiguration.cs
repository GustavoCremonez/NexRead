using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexRead.Domain.Entities;

namespace NexRead.Infra.EntitiesConfiguration;

public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenre>
{
    public void Configure(EntityTypeBuilder<BookGenre> builder)
    {
        builder.ToTable("BookGenres");

        builder.HasKey(bg => bg.Id);

        builder.Property(bg => bg.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(bg => new { bg.BookId, bg.GenreId })
            .IsUnique();
    }
}
