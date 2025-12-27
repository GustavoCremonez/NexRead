using NexRead.Domain.Entities;

namespace NexRead.Domain.Interfaces;

public interface IRecommendationRepository
{
    Task<IEnumerable<Book>> GetBooksByGenreIdsAsync(IEnumerable<int> genreIds, IEnumerable<int> excludeBookIds, int limit);
    Task<IEnumerable<Book>> GetTopRatedBooksAsync(int limit);
}
