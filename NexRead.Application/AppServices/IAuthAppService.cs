using NexRead.Application.Common;
using NexRead.Application.DTOs.Auth.Requests;
using NexRead.Application.DTOs.Auth.Responses;

namespace NexRead.Application.AppServices;

public interface IAuthAppService
{
    Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task<Result> LogoutAsync(string? refreshToken, CancellationToken cancellationToken = default);
}
