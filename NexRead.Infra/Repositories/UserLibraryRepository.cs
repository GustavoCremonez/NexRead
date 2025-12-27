using Microsoft.EntityFrameworkCore;
using NexRead.Domain.Entities;
using NexRead.Domain.Enums;
using NexRead.Domain.Interfaces;
using NexRead.Infra.Context;

namespace NexRead.Infra.Repositories;

public class UserLibraryRepository : IUserLibraryRepository
{
    private readonly ApplicationDbContext _context;

    public UserLibraryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserLibrary?> GetByUserAndBookAsync(Guid userId, int bookId)
    {
        return await _context.UserLibraries
            .Include(ul => ul.Book)
                .ThenInclude(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
            .Include(ul => ul.Book)
                .ThenInclude(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
            .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.BookId == bookId);
    }

    public async Task<IEnumerable<UserLibrary>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserLibraries
            .Where(ul => ul.UserId == userId)
            .Include(ul => ul.Book)
                .ThenInclude(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
            .Include(ul => ul.Book)
                .ThenInclude(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
            .OrderByDescending(ul => ul.AddedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserLibrary>> GetByUserIdAndStatusAsync(Guid userId, ReadingStatus status)
    {
        return await _context.UserLibraries
            .Where(ul => ul.UserId == userId && ul.Status == status)
            .Include(ul => ul.Book)
                .ThenInclude(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
            .Include(ul => ul.Book)
                .ThenInclude(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
            .OrderByDescending(ul => ul.AddedAt)
            .ToListAsync();
    }
}
