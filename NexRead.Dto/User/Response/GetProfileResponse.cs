namespace NexRead.Dto.User.Response;

/// <summary>
/// Response com dados do perfil do usuário
/// </summary>
/// <param name="Id">ID do usuário</param>
/// <param name="Name">Nome do usuário</param>
/// <param name="Email">Email do usuário</param>
/// <param name="CreatedAt">Data de criação da conta</param>
public sealed record GetProfileResponse(Guid Id, string Name, string Email, DateTime CreatedAt);
