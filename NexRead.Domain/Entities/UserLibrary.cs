using NexRead.Domain.Enums;
using NexRead.Domain.Exceptions;

namespace NexRead.Domain.Entities;

public class UserLibrary
{
    public UserLibrary(Guid userId, int bookId, ReadingStatus status)
    {
        UserId = userId;
        BookId = bookId;
        Status = status;
        AddedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }

    public Guid UserId { get; private set; }

    public int BookId { get; private set; }

    public ReadingStatus Status { get; private set; }

    public DateTime AddedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public User User { get; private set; } = null!;

    public Book Book { get; private set; } = null!;

    public void UpdateStatus(ReadingStatus newStatus)
    {
        if (Status == newStatus)
            throw new BadRequestException("The book already has this status.");

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
