# Google Books API Integration - Implementation Guide

## Overview
This directory contains the integration with Google Books API for searching and caching books.

## Current Status
✅ Basic structure created
❌ Full implementation pending

## Implementation Checklist

### 1. API Configuration
- [ ] Add Google Books API key to `appsettings.json`
- [ ] Configure HttpClient base address in `GoogleBooksClient.cs`
- [ ] Add rate limiting configuration
- [ ] Add retry policies (Polly)

### 2. Search Implementation (`SearchBooksAsync`)
- [ ] Build request URL: `https://www.googleapis.com/books/v1/volumes?q={query}&maxResults={limit}&key={apiKey}`
- [ ] Deserialize JSON response to Google Books models
- [ ] Map `VolumeInfo` to `BookResponse`
- [ ] Extract and normalize authors
- [ ] Extract and map categories to genres
- [ ] Handle pagination if needed
- [ ] Implement error handling

### 3. Get by ID Implementation (`GetBookByExternalIdAsync`)
- [ ] Build request URL: `https://www.googleapis.com/books/v1/volumes/{volumeId}?key={apiKey}`
- [ ] Deserialize response
- [ ] Map to `BookResponse`
- [ ] Handle not found scenarios

### 4. Caching Strategy
- [ ] Create service to cache searched books in database
- [ ] Check database before calling API (avoid duplicate API calls)
- [ ] Create or find existing authors by name
- [ ] Create or find existing genres by name
- [ ] Link books with authors and genres
- [ ] Store external ID (Google Books volume ID) for reference

### 5. Models
Create DTOs for Google Books API responses:
```csharp
// GoogleBooksModels.cs
public class GoogleBooksResponse
{
    public List<VolumeItem> Items { get; set; }
}

public class VolumeItem
{
    public string Id { get; set; }
    public VolumeInfo VolumeInfo { get; set; }
}

public class VolumeInfo
{
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string Description { get; set; }
    public ImageLinks ImageLinks { get; set; }
    public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
    public string PublishedDate { get; set; }
    public int PageCount { get; set; }
    public List<string> Categories { get; set; }
    public string Language { get; set; }
    public double? AverageRating { get; set; }
}

public class ImageLinks
{
    public string Thumbnail { get; set; }
}

public class IndustryIdentifier
{
    public string Type { get; set; } // ISBN_10, ISBN_13
    public string Identifier { get; set; }
}
```

### 6. Error Handling
- [ ] Handle API rate limits (429 Too Many Requests)
- [ ] Handle network errors
- [ ] Handle invalid responses
- [ ] Log errors appropriately

### 7. Testing
- [ ] Unit tests for mapping logic
- [ ] Integration tests with mocked API responses
- [ ] Manual testing with real API

## API Documentation
- Google Books API: https://developers.google.com/books/docs/v1/using
- Get API Key: https://console.cloud.google.com/apis/credentials

## Configuration Example

### appsettings.json
```json
{
  "ExternalApis": {
    "GoogleBooks": {
      "BaseUrl": "https://www.googleapis.com/books/v1/",
      "ApiKey": "your-api-key-here",
      "MaxResults": 40,
      "Timeout": 30
    }
  }
}
```

### DependencyInjection.cs
```csharp
// Uncomment when implemented:
services.AddHttpClient<IExternalBookApiClient, GoogleBooksClient>(client =>
{
    var baseUrl = configuration["ExternalApis:GoogleBooks:BaseUrl"];
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});
```

## Notes
- Google Books API has a free tier with rate limits
- ISBN can be ISBN-10 or ISBN-13
- Categories from Google Books should be mapped to our Genre entities
- Not all books have all fields (handle nullables)
- Published date format varies (handle parsing carefully)
