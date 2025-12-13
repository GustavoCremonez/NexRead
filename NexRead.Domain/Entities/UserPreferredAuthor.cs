namespace NexRead.Domain.Entities;

public class UserPreferredAuthor
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public User User { get; private set; }

    public int AuthorId { get; private set; }

    public Author Author { get; private set; }
}
