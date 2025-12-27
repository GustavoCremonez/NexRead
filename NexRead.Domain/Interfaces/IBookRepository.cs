using NexRead.Domain.Entities;

namespace NexRead.Domain.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetByIdWithDetailsAsync(int id);
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<IEnumerable<Book>> SearchByTitleAsync(string title, int limit = 20);
    Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId, int limit = 20);
}
