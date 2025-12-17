using NexRead.Application.Common;
using NexRead.Dto.Auth.Request;
using NexRead.Dto.Auth.Response;
using NexRead.Application.Services;
using NexRead.Domain.Entities;
using NexRead.Domain.Repositories;
using NexRead.Domain.ValueObjects;

namespace NexRead.Application.AppServices;

public class AuthAppService : IAuthAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthenticationService _authenticationService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthAppService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IAuthenticationService authenticationService,
        IRefreshTokenService refreshTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _authenticationService = authenticationService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(request.Email);
        var password = Password.Create(request.Password);

        var emailExists = await _userRepository.ExistsByEmailAsync(email.Value, cancellationToken);
        if (emailExists)
            return Result.Failure<RegisterResponse>("Email already registered");

        var passwordHash = _passwordHasher.HashPassword(password.Value);

        var user = User.Create(request.Name, email, passwordHash);

        await _userRepository.AddAsync(user, cancellationToken);

        var response = new RegisterResponse(user.Id, user.Name, user.Email);

        return Result.Success(response);
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(request.Email);

        var user = await _userRepository.GetByEmailAsync(email.Value, cancellationToken);
        if (user is null)
            return Result.Failure<LoginResponse>("Invalid credentials");

        var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        if (!isPasswordValid)
            return Result.Failure<LoginResponse>("Invalid credentials");

        await _authenticationService.SignInAsync(user.Id, user.Name, user.Email, cancellationToken);

        var refreshToken = _refreshTokenService.GenerateRefreshToken();
        await _refreshTokenService.StoreRefreshTokenAsync(
            user.Id,
            refreshToken,
            TimeSpan.FromDays(7),
            cancellationToken);

        var response = new LoginResponse(user.Id, user.Name, user.Email, refreshToken);

        return Result.Success(response);
    }

    public async Task<Result<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var userId = await _refreshTokenService.ValidateRefreshTokenAsync(request.RefreshToken, cancellationToken);
        if (userId is null)
            return Result.Failure<RefreshTokenResponse>("Invalid or expired refresh token");

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
        if (user is null)
            return Result.Failure<RefreshTokenResponse>("User not found");

        await _refreshTokenService.RevokeRefreshTokenAsync(request.RefreshToken, cancellationToken);

        await _authenticationService.SignInAsync(user.Id, user.Name, user.Email, cancellationToken);

        var newRefreshToken = _refreshTokenService.GenerateRefreshToken();
        await _refreshTokenService.StoreRefreshTokenAsync(
            user.Id,
            newRefreshToken,
            TimeSpan.FromDays(7),
            cancellationToken);

        var response = new RefreshTokenResponse(newRefreshToken);

        return Result.Success(response);
    }

    public async Task<Result> LogoutAsync(string? refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _authenticationService.GetCurrentUserId();

        if (userId.HasValue)
        {
            await _refreshTokenService.RevokeAllUserTokensAsync(userId.Value, cancellationToken);
        }

        if (!string.IsNullOrWhiteSpace(refreshToken))
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        }

        await _authenticationService.SignOutAsync(cancellationToken);

        return Result.Success();
    }
}
