using NexRead.Domain.Exceptions;

namespace NexRead.Domain.Entities;

public class Book
{
    public Book(
        string title,
        string? description,
        string? isbn,
        string? imageUrl,
        DateTime? publishedDate,
        int? pageCount,
        string? language,
        double? averageRating)
    {
        Validate(title);

        Title = title;
        Description = description;
        Isbn = isbn;
        ImageUrl = imageUrl;
        PublishedDate = publishedDate;
        PageCount = pageCount;
        Language = language;
        AverageRating = averageRating;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        BookAuthors = new List<BookAuthor>();
        BookGenres = new List<BookGenre>();
    }

    public int Id { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public string? Isbn { get; private set; }

    public string? ImageUrl { get; private set; }

    public DateTime? PublishedDate { get; private set; }

    public int? PageCount { get; private set; }

    public string? Language { get; private set; }

    public double? AverageRating { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<BookAuthor> BookAuthors { get; private set; }

    public ICollection<BookGenre> BookGenres { get; private set; }

    public void Update(
        string title,
        string? description,
        string? imageUrl,
        DateTime? publishedDate,
        int? pageCount,
        string? language,
        double? averageRating)
    {
        Validate(title);

        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        PublishedDate = publishedDate;
        PageCount = pageCount;
        Language = language;
        AverageRating = averageRating;
        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new BadRequestException("Title is required.");
        if (title.Length > 500)
            throw new BadRequestException("Title must contain a maximum of 500 characters.");
    }
}
