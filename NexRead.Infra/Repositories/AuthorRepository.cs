using Microsoft.EntityFrameworkCore;
using NexRead.Domain.Entities;
using NexRead.Domain.Interfaces;
using NexRead.Infra.Context;

namespace NexRead.Infra.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Author?> GetAuthorByNameAsync(string name)
    {
        return await _context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Name.Equals(name));
    }
}
