using Microsoft.EntityFrameworkCore;
using NexRead.Domain.Entities;
using NexRead.Domain.Interfaces;
using NexRead.Infra.Context;

namespace NexRead.Infra.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Books
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await _context.Books
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
            .FirstOrDefaultAsync(b => b.Isbn == isbn);
    }

    public async Task<IEnumerable<Book>> SearchByTitleAsync(string title, int limit = 20)
    {
        return await _context.Books
            .Where(b => b.Title.ToLower().Contains(title.ToLower()))
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId, int limit = 20)
    {
        return await _context.Books
            .Where(b => b.BookGenres.Any(bg => bg.GenreId == genreId))
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
            .OrderByDescending(b => b.AverageRating)
            .Take(limit)
            .ToListAsync();
    }
}
