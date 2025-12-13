using NexRead.Application.Common;
using NexRead.Application.Interfaces;
using NexRead.Application.Mappers;
using NexRead.Domain.Entities;
using NexRead.Domain.Exceptions;
using NexRead.Domain.Interfaces;
using NexRead.Dto.Author.Request;
using NexRead.Dto.Author.Response;

namespace NexRead.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IGeneralRepository<Author> _generalRepository;
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IGeneralRepository<Author> generalRepository, IAuthorRepository authorRepository)
    {
        _generalRepository = generalRepository;
        _authorRepository = authorRepository;
    }

    public async Task<Result<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest createAuthorRequest)
    {
        var author = await _authorRepository.GetAuthorByNameAsync(createAuthorRequest.Name);

        if (author is not null)
            throw new ConflictException("An author with this name already exists.");

        var newAuthor = new Author(Guid.NewGuid(), createAuthorRequest.Name);

        await _generalRepository.AddAsync(newAuthor);
        await _generalRepository.SaveChangesAsync();

        return Result.Success(GenericMapper<Author, AuthorResponse>.ToDto(newAuthor));
    }

    public async Task<Result<AuthorResponse>> UpdateAuthorAsync(UpdateAuthorRequest updateAuthorRequest)
    {
        var authorSameName = await _authorRepository.GetAuthorByNameAsync(updateAuthorRequest.Name);

        if (authorSameName is not null)
            throw new ConflictException("An author with this name already exists.");

        Author? existingAuthor = await _generalRepository.GetByIdAsync(updateAuthorRequest.Id);

        if (existingAuthor is null)
            throw new NotFoundException("Author not found.");

        existingAuthor.Update(updateAuthorRequest.Name);

        _generalRepository.Update(existingAuthor);
        await _generalRepository.SaveChangesAsync();

        return Result.Success(GenericMapper<Author, AuthorResponse>.ToDto(existingAuthor));
    }

    public async Task<Result<AuthorResponse>> GetAuthorAsync(Guid authorId)
    {
        var author = await _generalRepository.GetByIdAsync(authorId);

        if (author is null)
            throw new NotFoundException("Author not found.");

        return Result.Success(GenericMapper<Author, AuthorResponse>.ToDto(author));
    }

    public async Task<Result> DeleteAuthorAsync(Guid authorId)
    {
        var author = await _generalRepository.GetByIdAsync(authorId);

        if (author is null)
            throw new NotFoundException("Author not found.");

        _generalRepository.Delete(author);
        await _generalRepository.SaveChangesAsync();

        return Result.Success();
    }
}
