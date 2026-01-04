using Microsoft.Extensions.Configuration;
using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Application.Mappers;
using NexRead.Dto.Book.Response;
using System.Text.Json;

namespace NexRead.Infra.ExternalApis;

/// <summary>
/// Client for Google Books API integration
/// </summary>
public class GoogleBooksClient : IExternalBookApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string GOOGLE_BOOKS_API_KEY;

    public GoogleBooksClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        GOOGLE_BOOKS_API_KEY = _configuration["GoogleBooks:ApiKey"];
    }

    public async Task<Result<IEnumerable<BookResponse>>> SearchBooksAsync(string query, int limit = 10)
    {
        // TODO: Cache results in database if needed
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentException("Query cannot be empty.", nameof(query));

        limit = Math.Clamp(limit, 1, 40);

        var requestUrl = CreateSearchRequestUrl(query, limit);

        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException(
                $"Google Books API error: {response.StatusCode} - {error}");
        }

        var googleResponseString =
            await response.Content.ReadAsStringAsync();

        var googleResponse = JsonSerializer.Deserialize<ExternalBookResponse>(
            googleResponseString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (googleResponse?.Items == null)
            return Result.Success(Enumerable.Empty<BookResponse>());

        var books = googleResponse.Items
            .Where(googleBookItem => googleBookItem.VolumeInfo != null)
            .Select(googleBookItem =>
            {
                var googleVolumeInfo = googleBookItem.VolumeInfo!;

                return googleVolumeInfo.ToBookResponse();
            });

        return Result.Success(books);
    }

    private string CreateSearchRequestUrl(string query, int limit)
    {
        return $"volumes?q={Uri.EscapeDataString(query)}" +
            $"&maxResults={limit}" +
            $"&key={GOOGLE_BOOKS_API_KEY}";
    }

    public async Task<BookResponse?> GetBookByExternalIdAsync(string externalId)
    {
        // TODO: Implement Google Books API get by ID
        // 1. Build request URL: /volumes/{externalId}
        // 2. Make HTTP GET request
        // 3. Deserialize response
        // 4. Map Google Books item to BookResponse
        // 5. Cache result in database if needed

        await Task.CompletedTask;
        throw new NotImplementedException("Google Books get by ID not yet implemented. See TODO comments.");
    }

    // TODO: Add private methods for:
    // - Mapping Google Books VolumeInfo to BookResponse
    // - Extracting authors from Google Books format
    // - Extracting genres/categories
    // - Handling API errors and rate limiting
    // - Caching mechanism
}
