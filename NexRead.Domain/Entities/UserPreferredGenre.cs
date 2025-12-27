namespace NexRead.Domain.Entities;

public class UserPreferredGenre
{
    public int Id { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public int GenreId { get; private set; }

    public Genre Genre { get; private set; }
}
