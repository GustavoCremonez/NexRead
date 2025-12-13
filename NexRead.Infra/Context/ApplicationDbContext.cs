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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
