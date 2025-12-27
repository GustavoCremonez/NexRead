using Microsoft.EntityFrameworkCore;
using NexRead.Domain.Entities;

namespace NexRead.Infra.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
    public DbSet<UserPreferredGenre> UserPreferredGenres => Set<UserPreferredGenre>();
    public DbSet<UserPreferredAuthor> UserPreferredAuthors => Set<UserPreferredAuthor>();
    public DbSet<UserLibrary> UserLibraries => Set<UserLibrary>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
