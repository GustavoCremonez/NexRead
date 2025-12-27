namespace NexRead.Dto.Book.Request;

/// <summary>
/// Request for updating an existing book
/// </summary>
/// <param name="Title">Book title</param>
/// <param name="Description">Book description</param>
/// <param name="ImageUrl">Cover image URL</param>
/// <param name="PublishedDate">Publication date</param>
/// <param name="PageCount">Number of pages</param>
/// <param name="Language">Book language</param>
/// <param name="AverageRating">Average rating</param>
/// <param name="AuthorIds">List of author IDs</param>
/// <param name="GenreIds">List of genre IDs</param>
public sealed record UpdateBookRequest(
    string Title,
    string? Description,
    string? ImageUrl,
    DateTime? PublishedDate,
    int? PageCount,
    string? Language,
    double? AverageRating,
    int[] AuthorIds,
    int[] GenreIds);
