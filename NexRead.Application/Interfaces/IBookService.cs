using NexRead.Application.Common;
using NexRead.Dto.Book.Request;
using NexRead.Dto.Book.Response;

namespace NexRead.Application.Interfaces;

public interface IBookService
{
    Task<Result<BookResponse>> CreateBookAsync(CreateBookRequest request);
    Task<Result<BookResponse>> UpdateBookAsync(int id, UpdateBookRequest request);
    Task<Result<BookResponse>> GetBookAsync(int id);
    Task<Result<IEnumerable<BookResponse>>> SearchBooksByTitleAsync(string title, int limit = 20);
    Task<Result<IEnumerable<BookResponse>>> GetBooksByGenreAsync(int genreId, int limit = 20);
    Task<Result> DeleteBookAsync(int id);
}
