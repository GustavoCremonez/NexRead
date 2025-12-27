using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexRead.Api.Extensions;
using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Dto.Book.Response;
using System.Security.Claims;

namespace NexRead.Api.Controllers;

/// <summary>
/// Controller for book recommendations
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RecommendationsController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationsController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    /// <summary>
    /// Gets personalized book recommendations for the authenticated user
    /// </summary>
    /// <param name="limit">Maximum number of recommendations (default: 10)</param>
    /// <returns>List of recommended books</returns>
    /// <response code="200">Returns recommended books</response>
    /// <response code="404">User not found</response>
    /// <remarks>
    /// Recommendation algorithm:
    /// 1. Analyzes genres from books user is Reading or has Read
    /// 2. Finds books with matching genres not in user's library
    /// 3. Orders by average rating and genre match count
    /// 4. Falls back to top-rated books if user has no library
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<BookResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<BookResponse>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecommendations([FromQuery] int limit = 10)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var response = await _recommendationService.GetRecommendationsForUserAsync(userId, limit);
        return response.ToOkActionResult();
    }
}
