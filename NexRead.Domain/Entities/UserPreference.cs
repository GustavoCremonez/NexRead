namespace NexRead.Domain.Entities;

public class UserPreference
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public User User { get; private set; }

    public string[] PreferredMoods { get; private set; }

    public string[] PreferredLanguages { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
}
