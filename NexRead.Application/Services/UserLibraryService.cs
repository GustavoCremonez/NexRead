using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Domain.Entities;
using NexRead.Domain.Enums;
using NexRead.Domain.Exceptions;
using NexRead.Domain.Interfaces;
using NexRead.Domain.Repositories;
using NexRead.Dto.Author.Response;
using NexRead.Dto.Book.Response;
using NexRead.Dto.UserLibrary.Request;
using NexRead.Dto.UserLibrary.Response;

namespace NexRead.Application.Services;

public class UserLibraryService : IUserLibraryService
{
    private readonly IBaseRepository<UserLibrary> _userLibraryRepository;
    private readonly IUserLibraryRepository _userLibrarySpecificRepository;
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IUserRepository _userRepository;

    public UserLibraryService(
        IBaseRepository<UserLibrary> userLibraryRepository,
        IUserLibraryRepository userLibrarySpecificRepository,
        IBaseRepository<Book> bookRepository,
        IUserRepository userRepository)
    {
        _userLibraryRepository = userLibraryRepository;
        _userLibrarySpecificRepository = userLibrarySpecificRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<UserLibraryResponse>> AddBookToLibraryAsync(Guid userId, AddBookToLibraryRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("User not found.");

        var book = await _bookRepository.GetByIdAsync(request.BookId);
        if (book is null)
            throw new NotFoundException("Book not found.");

        var existingEntry = await _userLibrarySpecificRepository.GetByUserAndBookAsync(userId, request.BookId);
        if (existingEntry is not null)
            throw new ConflictException("This book is already in your library.");

        var userLibrary = new UserLibrary(userId, request.BookId, (ReadingStatus)request.Status);

        await _userLibraryRepository.AddAsync(userLibrary);
        await _userLibraryRepository.SaveChangesAsync();

        var created = await _userLibrarySpecificRepository.GetByUserAndBookAsync(userId, request.BookId);
        return Result.Success(MapToResponse(created!));
    }

    public async Task<Result<UserLibraryResponse>> UpdateBookStatusAsync(Guid userId, int bookId, UpdateBookStatusRequest request)
    {
        var userLibrary = await _userLibrarySpecificRepository.GetByUserAndBookAsync(userId, bookId);
        if (userLibrary is null)
            throw new NotFoundException("Book not found in your library.");

        userLibrary.UpdateStatus((ReadingStatus)request.Status);

        _userLibraryRepository.Update(userLibrary);
        await _userLibraryRepository.SaveChangesAsync();

        var updated = await _userLibrarySpecificRepository.GetByUserAndBookAsync(userId, bookId);
        return Result.Success(MapToResponse(updated!));
    }

    public async Task<Result> RemoveBookFromLibraryAsync(Guid userId, int bookId)
    {
        var userLibrary = await _userLibrarySpecificRepository.GetByUserAndBookAsync(userId, bookId);
        if (userLibrary is null)
            throw new NotFoundException("Book not found in your library.");

        _userLibraryRepository.Delete(userLibrary);
        await _userLibraryRepository.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<UserLibraryResponse>>> GetUserLibraryAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("User not found.");

        var library = await _userLibrarySpecificRepository.GetByUserIdAsync(userId);
        var responses = library.Select(MapToResponse);
        return Result.Success(responses);
    }

    public async Task<Result<IEnumerable<UserLibraryResponse>>> GetUserLibraryByStatusAsync(Guid userId, ReadingStatus status)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new NotFoundException("User not found.");

        var library = await _userLibrarySpecificRepository.GetByUserIdAndStatusAsync(userId, status);
        var responses = library.Select(MapToResponse);
        return Result.Success(responses);
    }

    private static UserLibraryResponse MapToResponse(UserLibrary userLibrary)
    {
        var authors = userLibrary.Book.BookAuthors
            .Select(ba => new AuthorResponse(ba.Author.Id, ba.Author.Name, ba.Author.CreatedAt, ba.Author.UpdatedAt))
            .ToArray();

        var genres = userLibrary.Book.BookGenres
            .Select(bg => new GenreResponse(bg.Genre.Id, bg.Genre.Name))
            .ToArray();

        var bookResponse = new BookResponse(
            userLibrary.Book.Id,
            userLibrary.Book.Title,
            userLibrary.Book.Description,
            userLibrary.Book.Isbn,
            userLibrary.Book.ImageUrl,
            userLibrary.Book.PublishedDate,
            userLibrary.Book.PageCount,
            userLibrary.Book.Language,
            userLibrary.Book.AverageRating,
            authors,
            genres,
            userLibrary.Book.CreatedAt,
            userLibrary.Book.UpdatedAt);

        return new UserLibraryResponse(
            userLibrary.Id,
            userLibrary.UserId,
            bookResponse,
            (int)userLibrary.Status,
            userLibrary.AddedAt,
            userLibrary.UpdatedAt);
    }
}
