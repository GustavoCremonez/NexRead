using NexRead.Dto.Book.Response;

namespace NexRead.Dto.UserLibrary.Response;

/// <summary>
/// User library entry response
/// </summary>
/// <param name="Id">Library entry ID</param>
/// <param name="UserId">User ID</param>
/// <param name="Book">Book details</param>
/// <param name="Status">Reading status (WantToRead = 1, Reading = 2, Read = 3)</param>
/// <param name="AddedAt">Date added to library</param>
/// <param name="UpdatedAt">Last update date</param>
public sealed record UserLibraryResponse(
    int Id,
    Guid UserId,
    BookResponse Book,
    int Status,
    DateTime AddedAt,
    DateTime UpdatedAt);
