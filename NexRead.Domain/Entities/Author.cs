namespace NexRead.Domain.Entities;

public class Author
{
    public Author(Guid id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        UserPreferredAuthors = new List<UserPreferredAuthor>();
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<UserPreferredAuthor> UserPreferredAuthors { get; private set; }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}
