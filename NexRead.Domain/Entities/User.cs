using NexRead.Domain.ValueObjects;
using NexRead.Domain.Exceptions;

namespace NexRead.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<UserPreference> UserPreferences { get; private set; }

    public ICollection<UserPreferredGenre> UserPreferredGenres { get; private set; }

    public ICollection<UserPreferredAuthor> UserPreferredAuthors { get; private set; }

    public ICollection<UserLibrary> UserLibraries { get; private set; }

    private User()
    {
        UserPreferences = new List<UserPreference>();
        UserPreferredGenres = new List<UserPreferredGenre>();
        UserPreferredAuthors = new List<UserPreferredAuthor>();
        UserLibraries = new List<UserLibrary>();
    }

    public static User Create(string name, ValueObjects.Email email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Name cannot be empty");

        if (name.Length < 2)
            throw new ValidationException("Name must be at least 2 characters long");

        if (name.Length > 100)
            throw new ValidationException("Name must not exceed 100 characters");

        var now = DateTime.UtcNow;

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.Value,
            PasswordHash = passwordHash,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Name cannot be empty");

        if (name.Length < 2)
            throw new ValidationException("Name must be at least 2 characters long");

        if (name.Length > 100)
            throw new ValidationException("Name must not exceed 100 characters");

        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}
