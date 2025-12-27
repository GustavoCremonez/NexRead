using NexRead.Dto.Book.Response;

namespace NexRead.Application.Interfaces;

/// <summary>
/// Interface for external book API clients (Google Books, Open Library, etc.)
/// </summary>
public interface IExternalBookApiClient
{
    /// <summary>
    /// Searches for books by title or author
    /// </summary>
    /// <param name="query">Search query</param>
    /// <param name="limit">Maximum results</param>
    /// <returns>List of book responses</returns>
    Task<IEnumerable<BookResponse>> SearchBooksAsync(string query, int limit = 10);

    /// <summary>
    /// Gets book details by external ID (e.g., Google Books ID)
    /// </summary>
    /// <param name="externalId">External book identifier</param>
    /// <returns>Book details</returns>
    Task<BookResponse?> GetBookByExternalIdAsync(string externalId);
}
