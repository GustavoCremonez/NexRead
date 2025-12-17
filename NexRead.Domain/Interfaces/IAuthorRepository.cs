using NexRead.Domain.Entities;

namespace NexRead.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author?> GetAuthorByNameAsync(string name);
}
