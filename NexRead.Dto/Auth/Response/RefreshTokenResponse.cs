namespace NexRead.Dto.Auth.Response;

/// <summary>
/// Response da renovação de token
/// </summary>
/// <param name="RefreshToken">Novo token de renovação</param>
public sealed record RefreshTokenResponse(string RefreshToken);
