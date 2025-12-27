namespace NexRead.Dto.UserLibrary.Request;

/// <summary>
/// Request for updating book status in user library
/// </summary>
/// <param name="Status">New reading status (WantToRead = 1, Reading = 2, Read = 3)</param>
public sealed record UpdateBookStatusRequest(int Status);
