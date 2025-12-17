namespace NexRead.Dto.Auth.Request;

/// <summary>
/// Request para logout
/// </summary>
/// <param name="RefreshToken">Refresh token opcional para revogar</param>
public sealed record LogoutRequest(string? RefreshToken);
