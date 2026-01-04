using NexRead.Application.Interfaces;
using NexRead.Domain.Entities;
using NexRead.Domain.Repositories;
using System.Security.Cryptography;

namespace NexRead.Infra.Services;

public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public async Task StoreRefreshTokenAsync(Guid userId, string token, TimeSpan validFor, CancellationToken cancellationToken = default)
    {
        var refreshToken = RefreshToken.Create(userId, token, validFor);
        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
    }

    public async Task<Guid?> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token, cancellationToken);

        if (refreshToken is null || !refreshToken.IsValid())
            return null;

        return refreshToken.UserId;
    }

    public async Task RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token, cancellationToken);
        if (refreshToken is null)
            return;

        refreshToken.Revoke();
        await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);
    }

    public async Task RevokeAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await _refreshTokenRepository.RevokeAllByUserIdAsync(userId, cancellationToken);
    }
}
