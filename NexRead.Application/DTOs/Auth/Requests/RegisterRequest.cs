namespace NexRead.Application.DTOs.Auth.Requests;

/// <summary>
/// Request para registro de novo usuário
/// </summary>
/// <param name="Name">Nome completo do usuário (mín: 2, máx: 100 caracteres)</param>
/// <param name="Email">Email do usuário (deve ser único no sistema)</param>
/// <param name="Password">Senha (mín: 8, máx: 40 caracteres, com maiúsculas, minúsculas, números e caracteres especiais)</param>
public sealed record RegisterRequest(string Name, string Email, string Password);
