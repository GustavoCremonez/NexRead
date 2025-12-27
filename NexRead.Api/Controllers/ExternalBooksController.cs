// TODO: Uncomment when GoogleBooksClient is fully implemented and registered in DI
// See: NexRead.Infra/ExternalApis/README_GOOGLE_BOOKS.md for implementation guide

/*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexRead.Application.Interfaces;
using NexRead.Dto.Book.Response;

namespace NexRead.Api.Controllers;

/// <summary>
/// Controller for searching books from external APIs
/// </summary>
[Route("api/external-books")]
[ApiController]
[Authorize]
public class ExternalBooksController : ControllerBase
{
    private readonly IExternalBookApiClient _externalBookApiClient;

    public ExternalBooksController(IExternalBookApiClient externalBookApiClient)
    {
        _externalBookApiClient = externalBookApiClient;
    }

    /// <summary>
    /// Searches for books in external APIs (Google Books)
    /// </summary>
    /// <param name="query">Search query (title, author, etc.)</param>
    /// <param name="limit">Maximum results (default: 10)</param>
    /// <returns>List of books from external API</returns>
    /// <response code="200">Returns list of books</response>
    /// <response code="501">Not implemented yet</response>
    /// <remarks>
    /// TODO: This endpoint will search Google Books API and cache results in the database.
    ///
    /// Implementation steps:
    /// 1. Call Google Books API
    /// 2. Map results to BookResponse
    /// 3. Cache books in database (authors, genres, etc.)
    /// 4. Return results to client
    /// </remarks>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<BookResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> SearchBooks([FromQuery] string query, [FromQuery] int limit = 10)
    {
        var results = await _externalBookApiClient.SearchBooksAsync(query, limit);
        return Ok(results);
    }

    /// <summary>
    /// Gets book details from external API by ID
    /// </summary>
    /// <param name="externalId">External book ID (e.g., Google Books ID)</param>
    /// <returns>Book details</returns>
    /// <response code="200">Returns book details</response>
    /// <response code="404">Book not found</response>
    /// <response code="501">Not implemented yet</response>
    [HttpGet("{externalId}")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> GetBookByExternalId(string externalId)
    {
        var book = await _externalBookApiClient.GetBookByExternalIdAsync(externalId);
        if (book is null)
            return NotFound();

        return Ok(book);
    }
}
*/
