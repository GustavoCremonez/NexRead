using NexRead.Domain.Exceptions;

namespace NexRead.Domain.Entities;

public class Author
{
    public Author(string name)
    {
        Validate(name);

        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        UserPreferredAuthors = new List<UserPreferredAuthor>();
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<UserPreferredAuthor> UserPreferredAuthors { get; private set; }

    public void Update(string name)
    {
        Validate(name);

        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new BadRequestException("Name is required.");
        if (name.Length <= 3)
            throw new BadRequestException("Name must contain a minimum of 3 characters.");
        if (name.Length > 100)
            throw new BadRequestException("Name must contain a maximum of 100 characters.");
    }
}
