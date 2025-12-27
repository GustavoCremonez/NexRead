using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Domain.Entities;
using NexRead.Domain.Enums;
using NexRead.Domain.Exceptions;
using NexRead.Domain.Interfaces;
using NexRead.Domain.Repositories;
using NexRead.Dto.Author.Response;
using NexRead.Dto.Book.Response;

namespace NexRead.Application.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserLibraryRepository _userLibraryRepository;
    private readonly IRecommendationRepository _recommendationRepository;

    public RecommendationService(
        IUserRepository userRepository,
        IUserLibraryRepository userLibraryRepository,
        IRecommendationRepository recommendationRepository)
    {
        _userRepository = userRepository;
        _userLibraryRepository = userLibraryRepository;
        _recommendationRepository = recommendationRepository;
    }

    public async Task<Result<IEnumerable<BookResponse>>> GetRecommendationsForUserAsync(Guid userId, int limit = 10)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("User not found.");

        var userLibrary = await _userLibraryRepository.GetByUserIdAsync(userId);
        var userBooks = userLibrary.ToList();

        if (!userBooks.Any())
        {
            return Result.Success(await GetTopRatedBooksAsync(limit));
        }

        var readingAndReadBooks = userBooks
            .Where(ul => ul.Status == ReadingStatus.Reading || ul.Status == ReadingStatus.Read)
            .ToList();

        if (!readingAndReadBooks.Any())
        {
            return Result.Success(await GetTopRatedBooksAsync(limit));
        }

        var genreIds = readingAndReadBooks
            .SelectMany(ul => ul.Book.BookGenres.Select(bg => bg.GenreId))
            .Distinct()
            .ToList();

        var userBookIds = userBooks.Select(ul => ul.BookId).ToList();

        var recommendations = await _recommendationRepository.GetBooksByGenreIdsAsync(genreIds, userBookIds, limit);

        if (!recommendations.Any())
        {
            return Result.Success(await GetTopRatedBooksAsync(limit));
        }

        var responses = recommendations.Select(MapToBookResponse);
        return Result.Success(responses);
    }

    private async Task<IEnumerable<BookResponse>> GetTopRatedBooksAsync(int limit)
    {
        var books = await _recommendationRepository.GetTopRatedBooksAsync(limit);
        return books.Select(MapToBookResponse);
    }

    private static BookResponse MapToBookResponse(Book book)
    {
        var authors = book.BookAuthors
            .Select(ba => new AuthorResponse(ba.Author.Id, ba.Author.Name, ba.Author.CreatedAt, ba.Author.UpdatedAt))
            .ToArray();

        var genres = book.BookGenres
            .Select(bg => new GenreResponse(bg.Genre.Id, bg.Genre.Name))
            .ToArray();

        return new BookResponse(
            book.Id,
            book.Title,
            book.Description,
            book.Isbn,
            book.ImageUrl,
            book.PublishedDate,
            book.PageCount,
            book.Language,
            book.AverageRating,
            authors,
            genres,
            book.CreatedAt,
            book.UpdatedAt);
    }
}
