using Microsoft.EntityFrameworkCore;
using NexRead.Domain.Entities;
using NexRead.Domain.Interfaces;
using NexRead.Infra.Context;

namespace NexRead.Infra.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Genre?> GetGenreByNameAsync(string name)
    {
        return await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
    }
}
