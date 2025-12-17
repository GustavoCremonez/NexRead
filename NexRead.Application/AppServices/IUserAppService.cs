using NexRead.Application.Common;
using NexRead.Dto.User.Response;

namespace NexRead.Application.AppServices;

public interface IUserAppService
{
    Task<Result<GetProfileResponse>> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<GetProfileResponse>> GetMyProfileAsync(CancellationToken cancellationToken = default);
}
