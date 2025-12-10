namespace NexRead.Domain.Entities;

public class UserPreference
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public string[] PreferredMoods { get; private set; }

    public string[] PreferredLanguages { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
}
