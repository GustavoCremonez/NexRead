namespace NexRead.Dto.Book.Request;

/// <summary>
/// Request for creating a new book
/// </summary>
/// <param name="Title">Book title</param>
/// <param name="Description">Book description</param>
/// <param name="Isbn">ISBN number</param>
/// <param name="ImageUrl">Cover image URL</param>
/// <param name="PublishedDate">Publication date</param>
/// <param name="PageCount">Number of pages</param>
/// <param name="Language">Book language</param>
/// <param name="AverageRating">Average rating</param>
/// <param name="AuthorIds">List of author IDs</param>
/// <param name="GenreIds">List of genre IDs</param>
public sealed record CreateBookRequest(
    string Title,
    string? Description,
    string? Isbn,
    string? ImageUrl,
    DateTime? PublishedDate,
    int? PageCount,
    string? Language,
    double? AverageRating,
    int[] AuthorIds,
    int[] GenreIds);
