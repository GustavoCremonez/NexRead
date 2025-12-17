namespace NexRead.Application.Services;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    Task StoreRefreshTokenAsync(Guid userId, string token, TimeSpan validFor, CancellationToken cancellationToken = default);
    Task<Guid?> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
    Task RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
    Task RevokeAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);
}
