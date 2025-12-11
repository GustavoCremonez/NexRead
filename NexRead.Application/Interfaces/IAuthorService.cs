using NexRead.Application.Common;
using NexRead.Dto.Author.Request;
using NexRead.Dto.Author.Response;

namespace NexRead.Application.Interfaces;

public interface IAuthorService
{
    Task<Result<AuthorResponse>> CreateAuthorAsync(AuthorRequest createAuthorDto);
}
