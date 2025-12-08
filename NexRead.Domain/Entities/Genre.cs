namespace NexRead.Domain.Entities;

public class Genre
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<UserPreferredGenre> UserPreferredGenres { get; private set; }
}
