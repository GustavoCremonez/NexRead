using NexRead.Dto.Author.Response;

namespace NexRead.Dto.Book.Response;

/// <summary>
/// Book details response
/// </summary>
/// <param name="Id">Book ID</param>
/// <param name="Title">Book title</param>
/// <param name="Description">Book description</param>
/// <param name="Isbn">ISBN number</param>
/// <param name="ImageUrl">Cover image URL</param>
/// <param name="PublishedDate">Publication date</param>
/// <param name="PageCount">Number of pages</param>
/// <param name="Language">Book language</param>
/// <param name="AverageRating">Average rating</param>
/// <param name="Authors">List of authors</param>
/// <param name="Genres">List of genres</param>
/// <param name="CreatedAt">Creation timestamp</param>
/// <param name="UpdatedAt">Last update timestamp</param>
public sealed record BookResponse(
    int Id,
    string Title,
    string? Description,
    string? Isbn,
    string? ImageUrl,
    DateTime? PublishedDate,
    int? PageCount,
    string? Language,
    double? AverageRating,
    AuthorResponse[] Authors,
    GenreResponse[] Genres,
    DateTime CreatedAt,
    DateTime UpdatedAt);
