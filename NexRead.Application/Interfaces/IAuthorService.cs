using NexRead.Application.Common;
using NexRead.Dto.Author.Request;
using NexRead.Dto.Author.Response;

namespace NexRead.Application.Interfaces;

public interface IAuthorService
{
    Task<Result<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest createAuthorRequest);

    Task<Result<AuthorResponse>> UpdateAuthorAsync(UpdateAuthorRequest updateAuthorRequest);

    Task<Result<AuthorResponse>> GetAuthorAsync(int authorId);

    Task<Result> DeleteAuthorAsync(int authorId);
}
