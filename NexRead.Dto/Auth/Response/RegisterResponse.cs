namespace NexRead.Dto.Auth.Response;

/// <summary>
/// Response do registro de usuário
/// </summary>
/// <param name="Id">ID único do usuário criado</param>
/// <param name="Name">Nome do usuário</param>
/// <param name="Email">Email do usuário</param>
public sealed record RegisterResponse(Guid Id, string Name, string Email);
