namespace NexRead.Dto.Auth.Request;

/// <summary>
/// Request para renovação de token de acesso
/// </summary>
/// <param name="RefreshToken">Token de renovação obtido no login</param>
public sealed record RefreshTokenRequest(string RefreshToken);
