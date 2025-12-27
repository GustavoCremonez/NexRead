using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Application.Mappers;
using NexRead.Domain.Entities;
using NexRead.Domain.Exceptions;
using NexRead.Domain.Interfaces;
using NexRead.Dto.Genre.Request;
using NexRead.Dto.Genre.Response;

namespace NexRead.Application.Services;

public class GenreService : IGenreService
{
    private readonly IBaseRepository<Genre> _genreRepository;
    private readonly IGenreRepository _genreSpecificRepository;

    public GenreService(IBaseRepository<Genre> genreRepository, IGenreRepository genreSpecificRepository)
    {
        _genreRepository = genreRepository;
        _genreSpecificRepository = genreSpecificRepository;
    }

    public async Task<Result<GenreResponse>> CreateGenreAsync(CreateGenreRequest request)
    {
        var existingGenre = await _genreSpecificRepository.GetGenreByNameAsync(request.Name);
        if (existingGenre is not null)
            throw new ConflictException("A genre with this name already exists.");

        var genre = new Genre(request.Name);

        await _genreRepository.AddAsync(genre);
        await _genreRepository.SaveChangesAsync();

        return Result.Success(GenericMapper<Genre, GenreResponse>.ToDto(genre));
    }

    public async Task<Result<GenreResponse>> UpdateGenreAsync(UpdateGenreRequest request)
    {
        var genreSameName = await _genreSpecificRepository.GetGenreByNameAsync(request.Name);
        if (genreSameName is not null && genreSameName.Id != request.Id)
            throw new ConflictException("A genre with this name already exists.");

        var existingGenre = await _genreRepository.GetByIdAsync(request.Id);
        if (existingGenre is null)
            throw new NotFoundException("Genre not found.");

        existingGenre.Update(request.Name);

        _genreRepository.Update(existingGenre);
        await _genreRepository.SaveChangesAsync();

        return Result.Success(GenericMapper<Genre, GenreResponse>.ToDto(existingGenre));
    }

    public async Task<Result<GenreResponse>> GetGenreAsync(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre is null)
            throw new NotFoundException("Genre not found.");

        return Result.Success(GenericMapper<Genre, GenreResponse>.ToDto(genre));
    }

    public async Task<Result<IEnumerable<GenreResponse>>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.GetAllAsync(asNoTracking: true);
        var responses = genres.Select(g => GenericMapper<Genre, GenreResponse>.ToDto(g));
        return Result.Success(responses);
    }

    public async Task<Result> DeleteGenreAsync(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre is null)
            throw new NotFoundException("Genre not found.");

        _genreRepository.Delete(genre);
        await _genreRepository.SaveChangesAsync();

        return Result.Success();
    }
}
