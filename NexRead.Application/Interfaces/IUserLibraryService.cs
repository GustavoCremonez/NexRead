using NexRead.Application.Common;
using NexRead.Domain.Enums;
using NexRead.Dto.UserLibrary.Request;
using NexRead.Dto.UserLibrary.Response;

namespace NexRead.Application.Interfaces;

public interface IUserLibraryService
{
    Task<Result<UserLibraryResponse>> AddBookToLibraryAsync(Guid userId, AddBookToLibraryRequest request);
    Task<Result<UserLibraryResponse>> UpdateBookStatusAsync(Guid userId, int bookId, UpdateBookStatusRequest request);
    Task<Result> RemoveBookFromLibraryAsync(Guid userId, int bookId);
    Task<Result<IEnumerable<UserLibraryResponse>>> GetUserLibraryAsync(Guid userId);
    Task<Result<IEnumerable<UserLibraryResponse>>> GetUserLibraryByStatusAsync(Guid userId, ReadingStatus status);
}
