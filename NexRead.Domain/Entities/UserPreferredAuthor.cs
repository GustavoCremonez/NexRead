namespace NexRead.Domain.Entities;

public class UserPreferredAuthor
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public Guid AuthorId { get; private set; }

    public Author Author { get; private set; }
}
