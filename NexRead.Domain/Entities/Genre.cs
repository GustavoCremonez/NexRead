namespace NexRead.Domain.Entities;

public class Genre
{
    public Genre(string name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        UserPreferredGenres = new List<UserPreferredGenre>();
        BookGenres = new List<BookGenre>();
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<UserPreferredGenre> UserPreferredGenres { get; private set; }

    public ICollection<BookGenre> BookGenres { get; private set; }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}
