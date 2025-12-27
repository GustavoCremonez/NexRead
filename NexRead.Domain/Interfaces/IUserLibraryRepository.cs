using NexRead.Domain.Entities;
using NexRead.Domain.Enums;

namespace NexRead.Domain.Interfaces;

public interface IUserLibraryRepository
{
    Task<UserLibrary?> GetByUserAndBookAsync(Guid userId, int bookId);
    Task<IEnumerable<UserLibrary>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<UserLibrary>> GetByUserIdAndStatusAsync(Guid userId, ReadingStatus status);
}
