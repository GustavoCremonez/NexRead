using Microsoft.AspNetCore.Mvc;
using NexRead.Api.Extensions;
using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Dto.Book.Request;
using NexRead.Dto.Book.Response;

namespace NexRead.Api.Controllers;

/// <summary>
/// Controller for book management
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Creates a new book
    /// </summary>
    /// <param name="request">Book data to be created</param>
    /// <returns>Created book data</returns>
    /// <response code="201">Book created successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="404">Author or Genre not found</response>
    /// <response code="409">Book with this ISBN already exists</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        var response = await _bookService.CreateBookAsync(request);
        return response.ToCreatedActionResult(response.Value.Id.ToString());
    }

    /// <summary>
    /// Updates an existing book
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <param name="request">Updated book data</param>
    /// <returns>Updated book data</returns>
    /// <response code="200">Book updated successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="404">Book, Author or Genre not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookRequest request)
    {
        var response = await _bookService.UpdateBookAsync(id, request);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Gets a book by ID
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <returns>Book data</returns>
    /// <response code="200">Returns book data</response>
    /// <response code="404">Book not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<BookResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(int id)
    {
        var response = await _bookService.GetBookAsync(id);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Searches books by title
    /// </summary>
    /// <param name="title">Search term</param>
    /// <param name="limit">Maximum number of results (default: 20)</param>
    /// <returns>List of matching books</returns>
    /// <response code="200">Returns list of books</response>
    [HttpGet("search")]
    [ProducesResponseType(typeof(Result<IEnumerable<BookResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchBooks([FromQuery] string title, [FromQuery] int limit = 20)
    {
        var response = await _bookService.SearchBooksByTitleAsync(title, limit);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Gets books by genre
    /// </summary>
    /// <param name="genreId">Genre ID</param>
    /// <param name="limit">Maximum number of results (default: 20)</param>
    /// <returns>List of books in the genre</returns>
    /// <response code="200">Returns list of books</response>
    /// <response code="404">Genre not found</response>
    [HttpGet("genre/{genreId}")]
    [ProducesResponseType(typeof(Result<IEnumerable<BookResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<BookResponse>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBooksByGenre(int genreId, [FromQuery] int limit = 20)
    {
        var response = await _bookService.GetBooksByGenreAsync(genreId, limit);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Deletes a book by ID
    /// </summary>
    /// <param name="id">Book ID to delete</param>
    /// <returns>Deletion confirmation</returns>
    /// <response code="204">Book deleted successfully</response>
    /// <response code="404">Book not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await _bookService.DeleteBookAsync(id);
        return response.ToNoContentActionResult();
    }
}
