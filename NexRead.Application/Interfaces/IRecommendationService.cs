using NexRead.Application.Common;
using NexRead.Dto.Book.Response;

namespace NexRead.Application.Interfaces;

public interface IRecommendationService
{
    Task<Result<IEnumerable<BookResponse>>> GetRecommendationsForUserAsync(Guid userId, int limit = 10);
}
