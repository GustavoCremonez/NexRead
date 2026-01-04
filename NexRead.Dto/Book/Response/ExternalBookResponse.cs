namespace NexRead.Dto.Book.Response;

public sealed class ExternalBookResponse
{
    public List<GoogleBookItem>? Items { get; set; }
}

public sealed class GoogleBookItem
{
    public GoogleVolumeInfo? VolumeInfo { get; set; }
}

public sealed class GoogleVolumeInfo
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PublishedDate { get; set; }
    public int? PageCount { get; set; }
    public string? Language { get; set; }
    public double? AverageRating { get; set; }
    public List<string>? Authors { get; set; }
    public GoogleImageLinks? ImageLinks { get; set; }
    public List<GoogleIndustryIdentifier>? IndustryIdentifiers { get; set; }
}

public sealed class GoogleImageLinks
{
    public string? Thumbnail { get; set; }
}

public sealed class GoogleIndustryIdentifier
{
    public string? Type { get; set; }
    public string? Identifier { get; set; }
}
