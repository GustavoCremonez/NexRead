namespace NexRead.Dto.Genre.Request;

/// <summary>
/// Request for updating an existing genre
/// </summary>
/// <param name="Id">Genre ID</param>
/// <param name="Name">Genre name</param>
public sealed record UpdateGenreRequest(int Id, string Name);
