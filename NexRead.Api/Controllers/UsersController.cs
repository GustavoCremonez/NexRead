using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexRead.Api.Extensions;
using NexRead.Application.AppServices;
using NexRead.Dto.User.Response;

namespace NexRead.Api.Controllers;

/// <summary>
/// Endpoints de gerenciamento de usuários
/// </summary>
[ApiController]
[Route("api/users")]
[Authorize]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserAppService _userAppService;

    public UsersController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    /// <summary>
    /// Busca o perfil do usuário autenticado
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados do perfil do usuário</returns>
    /// <response code="200">Perfil retornado com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <remarks>
    /// Retorna as informações básicas do perfil do usuário autenticado:
    /// - ID
    /// - Nome
    /// - Email
    /// - Data de criação
    /// - Data de última atualização
    /// </remarks>
    [HttpGet("me")]
    [ProducesResponseType(typeof(GetProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMyProfile(CancellationToken cancellationToken)
    {
        var result = await _userAppService.GetMyProfileAsync(cancellationToken);
        return result.ToActionResult();
    }

    /// <summary>
    /// Busca o perfil de um usuário específico por ID
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados do perfil do usuário</returns>
    /// <response code="200">Perfil retornado com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     GET /api/users/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// Retorna as informações básicas do perfil do usuário especificado.
    /// </remarks>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfile(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userAppService.GetProfileAsync(id, cancellationToken);
        return result.ToActionResult();
    }
}
