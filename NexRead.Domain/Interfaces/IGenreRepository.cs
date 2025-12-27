using NexRead.Domain.Entities;

namespace NexRead.Domain.Interfaces;

public interface IGenreRepository
{
    Task<Genre?> GetGenreByNameAsync(string name);
}
