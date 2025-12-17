using Microsoft.EntityFrameworkCore;
using NexRead.Domain.Entities;

namespace NexRead.Infra.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<UserPreference> UserPreferences { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<UserPreferredAuthor> UserPreferredAuthors { get; set; }

    public DbSet<UserPreferredGenre> UserPreferredGenres { get; set; }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
    public DbSet<UserPreferredGenre> UserPreferredGenres => Set<UserPreferredGenre>();
    public DbSet<UserPreferredAuthor> UserPreferredAuthors => Set<UserPreferredAuthor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
