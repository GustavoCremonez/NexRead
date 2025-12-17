namespace NexRead.Dto.Auth.Request;

/// <summary>
/// Request para autenticação de usuário
/// </summary>
/// <param name="Email">Email do usuário</param>
/// <param name="Password">Senha do usuário</param>
public sealed record LoginRequest(string Email, string Password);
