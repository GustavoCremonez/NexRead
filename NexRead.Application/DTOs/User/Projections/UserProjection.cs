namespace NexRead.Application.DTOs.User.Projections;

/// <summary>
/// Projeção de dados do usuário para consultas
/// </summary>
public sealed record UserProjection
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
