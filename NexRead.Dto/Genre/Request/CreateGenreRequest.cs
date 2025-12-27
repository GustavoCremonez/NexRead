namespace NexRead.Dto.Genre.Request;

/// <summary>
/// Request for creating a new genre
/// </summary>
/// <param name="Name">Genre name</param>
public sealed record CreateGenreRequest(string Name);
