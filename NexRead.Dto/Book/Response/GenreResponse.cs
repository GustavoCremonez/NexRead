namespace NexRead.Dto.Book.Response;

/// <summary>
/// Genre details response
/// </summary>
/// <param name="Id">Genre ID</param>
/// <param name="Name">Genre name</param>
public sealed record GenreResponse(int Id, string Name);
