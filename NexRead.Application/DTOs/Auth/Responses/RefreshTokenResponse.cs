namespace NexRead.Application.DTOs.Auth.Responses;

/// <summary>
/// Response da renovação de token
/// </summary>
/// <param name="RefreshToken">Novo token de renovação</param>
public sealed record RefreshTokenResponse(string RefreshToken);
