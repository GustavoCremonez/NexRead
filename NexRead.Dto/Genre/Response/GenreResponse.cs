namespace NexRead.Dto.Genre.Response;

/// <summary>
/// Genre details response
/// </summary>
/// <param name="Id">Genre ID</param>
/// <param name="Name">Genre name</param>
/// <param name="CreatedAt">Creation timestamp</param>
/// <param name="UpdatedAt">Last update timestamp</param>
public sealed record GenreResponse(int Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);
