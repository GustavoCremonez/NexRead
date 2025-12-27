using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexRead.Api.Extensions;
using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Domain.Enums;
using NexRead.Dto.UserLibrary.Request;
using NexRead.Dto.UserLibrary.Response;
using System.Security.Claims;

namespace NexRead.Api.Controllers;

/// <summary>
/// Controller for user library management
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserLibraryController : ControllerBase
{
    private readonly IUserLibraryService _userLibraryService;

    public UserLibraryController(IUserLibraryService userLibraryService)
    {
        _userLibraryService = userLibraryService;
    }

    /// <summary>
    /// Adds a book to user library
    /// </summary>
    /// <param name="request">Book and status information</param>
    /// <returns>Created library entry</returns>
    /// <response code="201">Book added to library successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="404">Book or User not found</response>
    /// <response code="409">Book already in library</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<UserLibraryResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<UserLibraryResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<UserLibraryResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<UserLibraryResponse>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddBookToLibrary([FromBody] AddBookToLibraryRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var response = await _userLibraryService.AddBookToLibraryAsync(userId, request);
        return response.ToCreatedActionResult(response.Value.Id.ToString());
    }

    /// <summary>
    /// Updates book status in user library
    /// </summary>
    /// <param name="bookId">Book ID</param>
    /// <param name="request">New status</param>
    /// <returns>Updated library entry</returns>
    /// <response code="200">Status updated successfully</response>
    /// <response code="400">Invalid data or same status</response>
    /// <response code="404">Book not found in library</response>
    [HttpPut("{bookId}")]
    [ProducesResponseType(typeof(Result<UserLibraryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserLibraryResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<UserLibraryResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBookStatus(int bookId, [FromBody] UpdateBookStatusRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var response = await _userLibraryService.UpdateBookStatusAsync(userId, bookId, request);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Removes a book from user library
    /// </summary>
    /// <param name="bookId">Book ID to remove</param>
    /// <returns>Deletion confirmation</returns>
    /// <response code="204">Book removed successfully</response>
    /// <response code="404">Book not found in library</response>
    [HttpDelete("{bookId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveBookFromLibrary(int bookId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var response = await _userLibraryService.RemoveBookFromLibraryAsync(userId, bookId);
        return response.ToNoContentActionResult();
    }

    /// <summary>
    /// Gets all books in user library
    /// </summary>
    /// <returns>List of library entries</returns>
    /// <response code="200">Returns library entries</response>
    /// <response code="404">User not found</response>
    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<UserLibraryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<UserLibraryResponse>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserLibrary()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var response = await _userLibraryService.GetUserLibraryAsync(userId);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Gets books by status in user library
    /// </summary>
    /// <param name="status">Reading status (WantToRead = 1, Reading = 2, Read = 3)</param>
    /// <returns>List of library entries with specified status</returns>
    /// <response code="200">Returns library entries</response>
    /// <response code="404">User not found</response>
    [HttpGet("status/{status}")]
    [ProducesResponseType(typeof(Result<IEnumerable<UserLibraryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<UserLibraryResponse>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserLibraryByStatus(ReadingStatus status)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var response = await _userLibraryService.GetUserLibraryByStatusAsync(userId, status);
        return response.ToOkActionResult();
    }
}
