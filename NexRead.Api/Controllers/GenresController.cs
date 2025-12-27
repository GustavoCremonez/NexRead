using Microsoft.AspNetCore.Mvc;
using NexRead.Api.Extensions;
using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Dto.Genre.Request;
using NexRead.Dto.Genre.Response;

namespace NexRead.Api.Controllers;

/// <summary>
/// Controller for genre management
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    /// <summary>
    /// Creates a new genre
    /// </summary>
    /// <param name="request">Genre data to be created</param>
    /// <returns>Created genre data</returns>
    /// <response code="201">Genre created successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="409">Genre with this name already exists</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
    {
        var response = await _genreService.CreateGenreAsync(request);
        return response.ToCreatedActionResult(response.Value.Id.ToString());
    }

    /// <summary>
    /// Updates an existing genre
    /// </summary>
    /// <param name="request">Updated genre data</param>
    /// <returns>Updated genre data</returns>
    /// <response code="200">Genre updated successfully</response>
    /// <response code="400">Invalid data</response>
    /// <response code="404">Genre not found</response>
    /// <response code="409">Genre with this name already exists</response>
    [HttpPut]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreRequest request)
    {
        var response = await _genreService.UpdateGenreAsync(request);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Gets a genre by ID
    /// </summary>
    /// <param name="id">Genre ID</param>
    /// <returns>Genre data</returns>
    /// <response code="200">Returns genre data</response>
    /// <response code="404">Genre not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<GenreResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGenre(int id)
    {
        var response = await _genreService.GetGenreAsync(id);
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Gets all genres
    /// </summary>
    /// <returns>List of all genres</returns>
    /// <response code="200">Returns list of genres</response>
    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<GenreResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllGenres()
    {
        var response = await _genreService.GetAllGenresAsync();
        return response.ToOkActionResult();
    }

    /// <summary>
    /// Deletes a genre by ID
    /// </summary>
    /// <param name="id">Genre ID to delete</param>
    /// <returns>Deletion confirmation</returns>
    /// <response code="204">Genre deleted successfully</response>
    /// <response code="404">Genre not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var response = await _genreService.DeleteGenreAsync(id);
        return response.ToNoContentActionResult();
    }
}
