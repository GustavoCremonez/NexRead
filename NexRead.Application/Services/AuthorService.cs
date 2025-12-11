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

    public async Task<Result<AuthorResponse>> CreateAuthorAsync(AuthorRequest createAuthorDto)
    {
        var author = await _authorRepository.GetAuthorByNameAsync(createAuthorDto.Name);

        if (author is not null)
            throw new ConflictException("An author with this name already exists.");

        var newAuthor = new Author(Guid.NewGuid(), createAuthorDto.Name);

        await _generalRepository.AddAsync(newAuthor);
        await _generalRepository.SaveChangesAsync();

        return Result.Success(GenericMapper<Author, AuthorResponse>.ToDto(newAuthor));
    }
}
