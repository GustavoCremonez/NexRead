using Microsoft.EntityFrameworkCore;
using NexRead.Domain.Entities;
using NexRead.Domain.Interfaces;
using NexRead.Infra.Context;

namespace NexRead.Infra.Repositories;

public class RecommendationRepository : IRecommendationRepository
{
    private readonly ApplicationDbContext _context;

    public RecommendationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetBooksByGenreIdsAsync(IEnumerable<int> genreIds, IEnumerable<int> excludeBookIds, int limit)
    {
        var genreIdsList = genreIds.ToList();
        var excludeBookIdsList = excludeBookIds.ToList();

        return await _context.Books
            .Where(b => !excludeBookIdsList.Contains(b.Id))
            .Where(b => b.BookGenres.Any(bg => genreIdsList.Contains(bg.GenreId)))
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
            .OrderByDescending(b => b.AverageRating ?? 0)
            .ThenByDescending(b => b.BookGenres.Count(bg => genreIdsList.Contains(bg.GenreId)))
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetTopRatedBooksAsync(int limit)
    {
        return await _context.Books
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
            .OrderByDescending(b => b.AverageRating ?? 0)
            .Take(limit)
            .ToListAsync();
    }
}
