namespace NexRead.Domain.Entities;

public class BookAuthor
{
    public BookAuthor(int bookId, int authorId)
    {
        BookId = bookId;
        AuthorId = authorId;
    }

    public int Id { get; private set; }

    public int BookId { get; private set; }

    public int AuthorId { get; private set; }

    public Book Book { get; private set; } = null!;

    public Author Author { get; private set; } = null!;
}
