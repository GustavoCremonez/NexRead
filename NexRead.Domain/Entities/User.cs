namespace NexRead.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public string PasswordSalt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<UserPreference> UserPreferences { get; private set; }

    public ICollection<UserPreferredGenre> UserPreferredGenres { get; private set; }

    public ICollection<UserPreferredAuthor> UserPreferredAuthors { get; private set; }
}
