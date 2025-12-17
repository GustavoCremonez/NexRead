using NexRead.Application.Common;
using NexRead.Application.DTOs.User.Responses;
using NexRead.Application.Services;
using NexRead.Domain.Repositories;

namespace NexRead.Application.AppServices;

public class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public UserAppService(
        IUserRepository userRepository,
        IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Result<GetProfileResponse>> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result.Failure<GetProfileResponse>("User not found");

        var response = new GetProfileResponse(
            user.Id,
            user.Name,
            user.Email,
            user.CreatedAt
        );

        return Result.Success(response);
    }

    public async Task<Result<GetProfileResponse>> GetMyProfileAsync(CancellationToken cancellationToken = default)
    {
        var userId = _authenticationService.GetCurrentUserId();
        if (userId is null)
            return Result.Failure<GetProfileResponse>("User not authenticated");

        return await GetProfileAsync(userId.Value, cancellationToken);
    }
}
