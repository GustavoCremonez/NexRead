using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Domain.Entities;
using NexRead.Domain.Exceptions;
using NexRead.Domain.Interfaces;
using NexRead.Dto.Author.Response;
using NexRead.Dto.Book.Request;
using NexRead.Dto.Book.Response;

namespace NexRead.Application.Services;

public class BookService : IBookService
{
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBookRepository _bookSpecificRepository;
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IBaseRepository<Genre> _genreRepository;
    private readonly IBaseRepository<BookAuthor> _bookAuthorRepository;
    private readonly IBaseRepository<BookGenre> _bookGenreRepository;

    public BookService(
        IBaseRepository<Book> bookRepository,
        IBookRepository bookSpecificRepository,
        IBaseRepository<Author> authorRepository,
        IBaseRepository<Genre> genreRepository,
        IBaseRepository<BookAuthor> bookAuthorRepository,
        IBaseRepository<BookGenre> bookGenreRepository)
    {
        _bookRepository = bookRepository;
        _bookSpecificRepository = bookSpecificRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
        _bookAuthorRepository = bookAuthorRepository;
        _bookGenreRepository = bookGenreRepository;
    }

    public async Task<Result<BookResponse>> CreateBookAsync(CreateBookRequest request)
    {
        if (request.Isbn is not null)
        {
            var existingBook = await _bookSpecificRepository.GetByIsbnAsync(request.Isbn);
            if (existingBook is not null)
                throw new ConflictException("A book with this ISBN already exists.");
        }

        var authors = new List<Author>();
        foreach (var authorId in request.AuthorIds)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author is null)
                throw new NotFoundException($"Author with ID {authorId} not found.");
            authors.Add(author);
        }

        var genres = new List<Genre>();
        foreach (var genreId in request.GenreIds)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre is null)
                throw new NotFoundException($"Genre with ID {genreId} not found.");
            genres.Add(genre);
        }

        var book = new Book(
            request.Title,
            request.Description,
            request.Isbn,
            request.ImageUrl,
            request.PublishedDate,
            request.PageCount,
            request.Language,
            request.AverageRating);

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        var bookAuthors = authors.Select(a => new BookAuthor(book.Id, a.Id)).ToList();
        await _bookAuthorRepository.AddRangeAsync(bookAuthors);

        var bookGenres = genres.Select(g => new BookGenre(book.Id, g.Id)).ToList();
        await _bookGenreRepository.AddRangeAsync(bookGenres);

        await _bookRepository.SaveChangesAsync();

        var createdBook = await _bookSpecificRepository.GetByIdWithDetailsAsync(book.Id);
        return Result.Success(MapToResponse(createdBook!));
    }

    public async Task<Result<BookResponse>> UpdateBookAsync(int id, UpdateBookRequest request)
    {
        var book = await _bookSpecificRepository.GetByIdWithDetailsAsync(id);
        if (book is null)
            throw new NotFoundException("Book not found.");

        var authors = new List<Author>();
        foreach (var authorId in request.AuthorIds)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author is null)
                throw new NotFoundException($"Author with ID {authorId} not found.");
            authors.Add(author);
        }

        var genres = new List<Genre>();
        foreach (var genreId in request.GenreIds)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre is null)
                throw new NotFoundException($"Genre with ID {genreId} not found.");
            genres.Add(genre);
        }

        book.Update(
            request.Title,
            request.Description,
            request.ImageUrl,
            request.PublishedDate,
            request.PageCount,
            request.Language,
            request.AverageRating);

        _bookRepository.Update(book);

        _bookAuthorRepository.DeleteRange(book.BookAuthors.ToList());
        _bookGenreRepository.DeleteRange(book.BookGenres.ToList());

        var bookAuthors = authors.Select(a => new BookAuthor(book.Id, a.Id)).ToList();
        await _bookAuthorRepository.AddRangeAsync(bookAuthors);

        var bookGenres = genres.Select(g => new BookGenre(book.Id, g.Id)).ToList();
        await _bookGenreRepository.AddRangeAsync(bookGenres);

        await _bookRepository.SaveChangesAsync();

        var updatedBook = await _bookSpecificRepository.GetByIdWithDetailsAsync(book.Id);
        return Result.Success(MapToResponse(updatedBook!));
    }

    public async Task<Result<BookResponse>> GetBookAsync(int id)
    {
        var book = await _bookSpecificRepository.GetByIdWithDetailsAsync(id);
        if (book is null)
            throw new NotFoundException("Book not found.");

        return Result.Success(MapToResponse(book));
    }

    public async Task<Result<IEnumerable<BookResponse>>> SearchBooksByTitleAsync(string title, int limit = 20)
    {
        var books = await _bookSpecificRepository.SearchByTitleAsync(title, limit);
        var responses = books.Select(MapToResponse);
        return Result.Success(responses);
    }

    public async Task<Result<IEnumerable<BookResponse>>> GetBooksByGenreAsync(int genreId, int limit = 20)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId);
        if (genre is null)
            throw new NotFoundException("Genre not found.");

        var books = await _bookSpecificRepository.GetByGenreIdAsync(genreId, limit);
        var responses = books.Select(MapToResponse);
        return Result.Success(responses);
    }

    public async Task<Result> DeleteBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
            throw new NotFoundException("Book not found.");

        _bookRepository.Delete(book);
        await _bookRepository.SaveChangesAsync();

        return Result.Success();
    }

    private static BookResponse MapToResponse(Book book)
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
