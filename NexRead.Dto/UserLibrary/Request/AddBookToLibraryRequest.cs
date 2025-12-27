namespace NexRead.Dto.UserLibrary.Request;

/// <summary>
/// Request for adding a book to user library
/// </summary>
/// <param name="BookId">Book ID</param>
/// <param name="Status">Reading status (WantToRead = 1, Reading = 2, Read = 3)</param>
public sealed record AddBookToLibraryRequest(int BookId, int Status);
