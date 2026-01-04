using NexRead.Dto.Author.Response;
using NexRead.Dto.Book.Response;

namespace NexRead.Application.Mappers;

public static class GoogleVolumeInfoMapper
{
    public static BookResponse ToBookResponse(this GoogleVolumeInfo googleVolumeInfo)
    {
        return new BookResponse(
            Id: 0,
            Title: googleVolumeInfo.Title ?? "Unknown title",
            Description: googleVolumeInfo.Description,
            Isbn: googleVolumeInfo.IndustryIdentifiers?
                .FirstOrDefault(x => x.Type == "ISBN_13")?.Identifier,
            ImageUrl: googleVolumeInfo.ImageLinks?.Thumbnail,
            PublishedDate: ParseDate(googleVolumeInfo.PublishedDate),
            PageCount: googleVolumeInfo.PageCount,
            Language: googleVolumeInfo.Language,
            AverageRating: googleVolumeInfo.AverageRating,
            Authors: Array.Empty<AuthorResponse>(),
            Genres: Array.Empty<GenreResponse>(),
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );
    }

    private static DateTime? ParseDate(string? date)
    {
        if (DateTime.TryParse(date, out var parsed))
            return parsed;

        return null;
    }
}
