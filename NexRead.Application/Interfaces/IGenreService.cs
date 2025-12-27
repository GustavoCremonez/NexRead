using NexRead.Application.Common;
using NexRead.Dto.Genre.Request;
using NexRead.Dto.Genre.Response;

namespace NexRead.Application.Interfaces;

public interface IGenreService
{
    Task<Result<GenreResponse>> CreateGenreAsync(CreateGenreRequest request);
    Task<Result<GenreResponse>> UpdateGenreAsync(UpdateGenreRequest request);
    Task<Result<GenreResponse>> GetGenreAsync(int id);
    Task<Result<IEnumerable<GenreResponse>>> GetAllGenresAsync();
    Task<Result> DeleteGenreAsync(int id);
}
