using NexRead.Application.Common;
using NexRead.Application.DTOs.User.Responses;

namespace NexRead.Application.AppServices;

public interface IUserAppService
{
    Task<Result<GetProfileResponse>> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<GetProfileResponse>> GetMyProfileAsync(CancellationToken cancellationToken = default);
}
