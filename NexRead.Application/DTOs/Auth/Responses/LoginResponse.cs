namespace NexRead.Application.DTOs.Auth.Responses;

/// <summary>
/// Response do login de usuário
/// </summary>
/// <param name="Id">ID do usuário autenticado</param>
/// <param name="Name">Nome do usuário</param>
/// <param name="Email">Email do usuário</param>
/// <param name="RefreshToken">Token para renovação da sessão</param>
public sealed record LoginResponse(Guid Id, string Name, string Email, string RefreshToken);
