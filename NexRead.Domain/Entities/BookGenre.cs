namespace NexRead.Domain.Entities;

public class BookGenre
{
    public BookGenre(int bookId, int genreId)
    {
        BookId = bookId;
        GenreId = genreId;
    }

    public int Id { get; private set; }

    public int BookId { get; private set; }

    public int GenreId { get; private set; }

    public Book Book { get; private set; } = null!;

    public Genre Genre { get; private set; } = null!;
}
